/*
 * Steper.c
 *
 * Created: 2.8.2018 11:20:35
 *  Author: Bozic
 */ 

 #include "Steper.h"
 #include <avr/interrupt.h>
 #include <stdbool.h>

volatile long int CurrPosSteper1=0,CurrPosSteper2=0;

volatile long int ZadatiBrojKoraka1=0,TrenBrojkoraka1=0,ZadatiBrojKoraka2=0,TrenBrojkoraka2=0;

int SaveBrzina1=0,SaveBrzina2=0;

int SpeedSteper1=100;
int SpeedSteper2=100;

volatile bool Steper1Start=false;
volatile bool Steper2Start=false;

enum smer SmerSteper1=Napred;
enum smer SmerSteper2=Napred;


void InicStartSwitch()
{
	DDRD &= ~(1<<4);
	DDRD &= ~(1<<3);
}

void timer1_init() // Svakih 1mS na 8MHz
{
	// set up timer with prescaler = 256
	TCCR1B |= (1 << CS12);
	
	// initialize counter
	TCNT1 = 0;
	
	// enable overflow interrupt
	TIMSK |= (1 << TOIE1);

}

void timer2_init()
{
	// set up timer with prescaler = 256
	TCCR2 |= (1 << CS22)|(1 << CS21)|(1 << CS20);
	
	// initialize counter
	TCNT2 = 0;
	
	// enable overflow interrupt
	TIMSK |= (1 << TOIE2);
}

 void InicSteper1()
 {
	DDRA |= (1<<0);
	DDRA |= (1<<1);
	DDRA |= (1<<2);
	DDRA |= (1<<3);

	Steper1A_OFF;
	Steper1B_OFF;
	Steper1C_OFF;
	Steper1D_OFF;
 }

 void InicSteper2()
 {
	DDRB |= (1<<0);
	DDRB |= (1<<1);
	DDRB |= (1<<2);
	DDRB |= (1<<3);

	Steper2A_OFF;
	Steper2B_OFF;
	Steper2C_OFF;
	Steper2D_OFF;
 }

 int Steper1Stop()
 {
	Steper1Start=false;

	long int Ret=TrenBrojkoraka1;
	TrenBrojkoraka1=0;

	return Ret;
 }

 int Steper2Stop()
 {
	Steper2Start=false;

	long int Ret=TrenBrojkoraka2;
	TrenBrojkoraka2=0;

	return Ret;
 }

 bool Steper1Ready()
 {
	return (!Steper1Start);
 }

 bool Steper2Ready()
 {
	return (!Steper2Start);
 }

 void Steper1Run(int Speed,enum smer SMR,long int BrKoraka) //Speed u Hz
 {
	SaveBrzina1=65535-(int)(1.0/(0.000032*Speed));
	TCNT1=SaveBrzina1;

	SmerSteper1=SMR;

	TrenBrojkoraka1=0;
	ZadatiBrojKoraka1=BrKoraka;

	Steper1Start=true;
 }

  void Steper2Run(int Speed,enum smer SMR,long int BrKoraka) //Speed u Hz
  {
	  SaveBrzina2=256-(int)(1.0/(0.000128*Speed));
	  TCNT2=SaveBrzina2;

	  SmerSteper2=SMR;

	  TrenBrojkoraka2=0;
	  ZadatiBrojKoraka2=BrKoraka;

	  Steper2Start=true;
  }

  bool StepersToStart()
  {

	bool End1=false, End2=false;

	Steper1Run(400,Nazad,90000);
	Steper2Run(400,Nazad,90000);
	
	while(!End1 || !End2)
	{
		if(Switch2IsSet)
			{Steper2Start=false; End1=true;}
		if(Switch1IsSet)
			{Steper1Start=false; End2=true;}
	}

	return true;
  }

  void Steper1SmerNapred()
  {
	SmerSteper1=Napred;
  }

  void Steper2SmerNapred()
  {
	SmerSteper2=Napred;
  }

 ISR(TIMER1_OVF_vect) 
 {
	TCNT1=SaveBrzina1;

	if(TrenBrojkoraka1>(ZadatiBrojKoraka1-1))
	{
		Steper1Start=false;
		Steper1A_OFF;
		Steper1B_OFF;
		Steper1C_OFF;
		Steper1D_OFF;
	}

	if(SmerSteper1==Nazad && Steper1Start)		//ovde menjan smer
		{
		TrenBrojkoraka1++;
		switch(CurrPosSteper1)
		{
			case 0:
				Steper1A_ON;
				Steper1B_ON;
				Steper1C_OFF;
				Steper1D_OFF;
				CurrPosSteper1=1;
				break;
			case 1:
				Steper1A_OFF;
				Steper1B_ON;
				Steper1C_OFF;
				Steper1D_OFF;
				CurrPosSteper1=2;
				break;
			case 2:
				Steper1A_OFF;
				Steper1B_ON;
				Steper1C_ON;
				Steper1D_OFF;
				CurrPosSteper1=3;
				break;
			case 3:
				Steper1A_OFF;
				Steper1B_OFF;
				Steper1C_ON;
				Steper1D_OFF;
				CurrPosSteper1=4;
				break;
			case 4:
				Steper1A_OFF;
				Steper1B_OFF;
				Steper1C_ON;
				Steper1D_ON;
				CurrPosSteper1=5;
				break;
			case 5:
				Steper1A_OFF;
				Steper1B_OFF;
				Steper1C_OFF;
				Steper1D_ON;
				CurrPosSteper1=6;
				break;
			case 6:
				Steper1A_ON;
				Steper1B_OFF;
				Steper1C_OFF;
				Steper1D_ON;
				CurrPosSteper1=7;
				break;
			case 7:
				Steper1A_ON;
				Steper1B_OFF;
				Steper1C_OFF;
				Steper1D_OFF;
				CurrPosSteper1=0;
				break;
		}
		}

		if(SmerSteper1==Napred && Steper1Start)		//ovde menjan Smer
		{
        TrenBrojkoraka1++;
		switch(CurrPosSteper1)
		{
			case 0:
				Steper1A_OFF;
				Steper1B_OFF;
				Steper1C_ON;
				Steper1D_ON;
				CurrPosSteper1=1;
				break;
			case 1:
				Steper1A_OFF;
				Steper1B_OFF;
				Steper1C_ON;
				Steper1D_OFF;
				CurrPosSteper1=2;
				break;
			case 2:
				Steper1A_OFF;
				Steper1B_ON;
				Steper1C_ON;
				Steper1D_OFF;
				CurrPosSteper1=3;
				break;
			case 3:
				Steper1A_OFF;
				Steper1B_ON;
				Steper1C_OFF;
				Steper1D_OFF;
				CurrPosSteper1=4;
				break;
			case 4:
				Steper1A_ON;
				Steper1B_ON;
				Steper1C_OFF;
				Steper1D_OFF;
				CurrPosSteper1=5;
				break;
			case 5:
				Steper1A_ON;
				Steper1B_OFF;
				Steper1C_OFF;
				Steper1D_OFF;
				CurrPosSteper1=6;
				break;
			case 6:
				Steper1A_ON;
				Steper1B_OFF;
				Steper1C_OFF;
				Steper1D_ON;
				CurrPosSteper1=7;
				break;
			case 7:
				Steper1A_OFF;
				Steper1B_OFF;
				Steper1C_OFF;
				Steper1D_ON;
				CurrPosSteper1=0;
				break;
		}
		}
 }

 ISR(TIMER2_OVF_vect)
 {
	TCNT2=SaveBrzina2;

	if(TrenBrojkoraka2>(ZadatiBrojKoraka2-1))
	{
		Steper2Start=false;
		Steper2A_OFF;
		Steper2B_OFF;
		Steper2C_OFF;
		Steper2D_OFF;
	}

	if(SmerSteper2==Napred && Steper2Start)
	{
		TrenBrojkoraka2++;
		switch(CurrPosSteper2)
		{
			case 0:
				Steper2A_ON;
				Steper2B_ON;
				Steper2C_OFF;
				Steper2D_OFF;
				CurrPosSteper2=1;
				break;
			case 1:
				Steper2A_OFF;
				Steper2B_ON;
				Steper2C_OFF;
				Steper2D_OFF;
				CurrPosSteper2=2;
				break;
			case 2:
				Steper2A_OFF;
				Steper2B_ON;
				Steper2C_ON;
				Steper2D_OFF;
				CurrPosSteper2=3;
				break;
			case 3:
				Steper2A_OFF;
				Steper2B_OFF;
				Steper2C_ON;
				Steper2D_OFF;
				CurrPosSteper2=4;
				break;
			case 4:
				Steper2A_OFF;
				Steper2B_OFF;
				Steper2C_ON;
				Steper2D_ON;
				CurrPosSteper2=5;
				break;
			case 5:
				Steper2A_OFF;
				Steper2B_OFF;
				Steper2C_OFF;
				Steper2D_ON;
				CurrPosSteper2=6;
				break;
			case 6:
				Steper2A_ON;
				Steper2B_OFF;
				Steper2C_OFF;
				Steper2D_ON;
				CurrPosSteper2=7;
				break;
			case 7:
				Steper2A_ON;
				Steper2B_OFF;
				Steper2C_OFF;
				Steper2D_OFF;
				CurrPosSteper2=0;
				break;
		}
	}


	if(SmerSteper2==Nazad && Steper2Start)
	{
		TrenBrojkoraka2++;
		switch(CurrPosSteper2)
		{
					case 0:
						Steper2A_OFF;
						Steper2B_OFF;
						Steper2C_ON;
						Steper2D_ON;
						CurrPosSteper2=1;
						break;
					case 1:
						Steper2A_OFF;
						Steper2B_OFF;
						Steper2C_ON;
						Steper2D_OFF;
						CurrPosSteper2=2;
						break;
					case 2:
						Steper2A_OFF;
						Steper2B_ON;
						Steper2C_ON;
						Steper2D_OFF;
						CurrPosSteper2=3;
						break;
					case 3:
						Steper2A_OFF;
						Steper2B_ON;
						Steper2C_OFF;
						Steper2D_OFF;
						CurrPosSteper2=4;
						break;
					case 4:
						Steper2A_ON;
						Steper2B_ON;
						Steper2C_OFF;
						Steper2D_OFF;
						CurrPosSteper2=5;
						break;
					case 5:
						Steper2A_ON;
						Steper2B_OFF;
						Steper2C_OFF;
						Steper2D_OFF;
						CurrPosSteper2=6;
						break;
					case 6:
						Steper2A_ON;
						Steper2B_OFF;
						Steper2C_OFF;
						Steper2D_ON;
						CurrPosSteper2=7;
						break;
					case 7:
						Steper2A_OFF;
						Steper2B_OFF;
						Steper2C_OFF;
						Steper2D_ON;
						CurrPosSteper2=0;
						break;
		}
	}
 }
