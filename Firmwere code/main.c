/*
 * ATmega32Code.c
 *
 * Created: 30.7.2018 17:34:25
 * Author : Bozic
 */ 

#define F_CPU 8000000UL
#include <avr/io.h>
#include <util/delay.h>
#include "Servo.h"
#include "Steper.h"
#include <avr/interrupt.h>
#include "Uartavr.h"
#include <stdbool.h>


bool moze=false,NijePoslato=false;

int main(void)
{
	inicServo();
	InicSteper1();
	InicSteper2();
	Uart_inic();
	inicMotorAndFan();

	MOTORSet(1);
	FANSet(1);

	uart_clear();

	timer1_init();
	timer2_init();

	sei();

	ServoSet(55); // do 55 sa oscilatorom 8MHz  //Podigni Burgiju


    while (1) 
    {
		if(Switch2IsSet)			//Ako se nekako dodje do starta Gasi motore ...
			Steper2SmerNapred(); 
		if(Switch1IsSet)
			Steper1SmerNapred();

		if(NijePoslato && Steper1Ready() && Steper2Ready() && ServoReady())  // Kad je sve spremno salji "Ready" 
		{
			uart_send_string("READY");
			NijePoslato = false;
		}


		if(uart_full())
		{

			if(Resive[0]=='A' && Resive[1]=='A')		// Start
			uart_send_string("AA");

			if(Resive[0]=='T' && Resive[1]=='E' && Resive[2]=='S' && Resive[3]=='T')		// Test
			uart_send_string("TEST");

			if(Resive[0]=='G' && Resive[1]=='L' && Resive[2]=='M')		// Brzina GL motora
			{
				int value=0;
				value+=(Resive[3]-48)*100;
				value+=(Resive[4]-48)*10;
				value+=(Resive[5]-48);

				MOTORSet(value-100);
			}

			if(Resive[0]=='S' && Resive[1]=='T' && Resive[2]=='P')		//STOP
			{
				MOTORSet(1);
				FANSet(1);
				Steper1Stop();
				Steper2Stop();
			}

			if(Resive[0]=='N' && Resive[1]=='S' && Resive[2]=='1') //Napred start motor1
			{
				int value=0;
				value+=(Resive[3]-48)*100;
				value+=(Resive[4]-48)*10;
				value+=(Resive[5]-48);

				Steper1Run(value,Napred,20000);
				moze=true;
			}

			if(Resive[0]=='B' && Resive[1]=='S' && Resive[2]=='1') //Napred start motor1
			{
				int value=0;
				value+=(Resive[3]-48)*100;
				value+=(Resive[4]-48)*10;
				value+=(Resive[5]-48);

				Steper1Run(value,Nazad,20000);
				moze=true;
			}

			if(Resive[0]=='N' && Resive[1]=='S' && Resive[2]=='2') //Napred start motor1
			{
				int value=0;
				value+=(Resive[3]-48)*100;
				value+=(Resive[4]-48)*10;
				value+=(Resive[5]-48);

				Steper2Run(value,Napred,20000);
				moze=true;
			}

			if(Resive[0]=='B' && Resive[1]=='S' && Resive[2]=='2') //Napred start motor1
			{
				int value=0;
				value+=(Resive[3]-48)*100;
				value+=(Resive[4]-48)*10;
				value+=(Resive[5]-48);

				Steper2Run(value,Nazad,20000);
				moze=true;
			}

			if(Resive[0]=='S' && Resive[1]=='M' && Resive[2]=='1')	//Stop motor 1
			{
				if(moze)
				{
				int value=Steper1Stop();
				char val[6];
				val[0]=(value/10000)+48;
				val[1]=((value%10000)/1000)+48;
				val[2]=(((value%10000)%1000)/100)+48;
				val[3]=((((value%10000)%1000)%100)/10)+48;
				val[4]=((((value%10000)%1000)%100)%10)+48;
				val[5]='\0';
				uart_send_string(val);
				}
				moze=false;
			}

			if(Resive[0]=='S' && Resive[1]=='M' && Resive[2]=='2')	//Stop motor 2
			{
				if(moze)
				{
				int value=Steper2Stop();
				char val[6];
				val[0]=(value/10000)+48;
				val[1]=((value%10000)/1000)+48;
				val[2]=(((value%10000)%1000)/100)+48;
				val[3]=((((value%10000)%1000)%100)/10)+48;
				val[4]=((((value%10000)%1000)%100)%10)+48;
				val[5]='\0';
				uart_send_string(val);
				}
				moze=false;
			}

			if(Resive[0]=='F' && Resive[1]=='A' && Resive[2]=='N')	// FAN brzina
			{
				int value=0;
				value+=(Resive[3]-48)*100;
				value+=(Resive[4]-48)*10;
				value+=(Resive[5]-48);

				FANSet(value-100);
			}

			if(Resive[0]=='S' && Resive[1]=='T' && Resive[2]=='R') // Go To Start
			{
				ServoSet(50);
				StepersToStart();
				
			}

			if(Resive[0]=='Z' && Resive[1]=='O')  // Slati od 35-50 za Z osu
			{
				NijePoslato=true;

				int value=0;

				value+=(Resive[2]-48)*10;
				value+=(Resive[3]-48);

				ServoSet(value);
			}
			
			if(Resive[0]=='M' && Resive[1]=='A')  // MA 00000 BBB S 0000 BBB S
			{
				NijePoslato=true;

				int BrojKorakaMororA=0;
				int BrojKorakaMororB=0;

				int SpeedA=0;
				int SpeedB=0;

				enum smer SmerA,SmerB;

				BrojKorakaMororA += (Resive[2]-48)*10000;
				BrojKorakaMororA += (Resive[3]-48)*1000;
				BrojKorakaMororA += (Resive[4]-48)*100;
				BrojKorakaMororA += (Resive[5]-48)*10;
				BrojKorakaMororA += (Resive[6]-48);

				SpeedA += (Resive[7]-48)*100;
				SpeedA += (Resive[8]-48)*10;
				SpeedA += (Resive[9]-48)*1;

				if(Resive[10]=='U') // Up
				{
					SmerA=Nazad;
				}
				else SmerA=Napred;

				BrojKorakaMororB += (Resive[11]-48)*10000;
				BrojKorakaMororB += (Resive[12]-48)*1000;
				BrojKorakaMororB += (Resive[13]-48)*100;
				BrojKorakaMororB += (Resive[14]-48)*10;
				BrojKorakaMororB += (Resive[15]-48);
				
				SpeedB += (Resive[16]-48)*100;
				SpeedB += (Resive[17]-48)*10;
				SpeedB += (Resive[18]-48)*1;

				if(Resive[19]=='U') // Up
				{
					SmerB=Nazad;
				}
				else SmerB=Napred;

				if(Steper1Ready())
				{
					Steper1Run(SpeedA,SmerA,BrojKorakaMororA);
				}
				if(Steper2Ready())
				{
					Steper2Run(SpeedB,SmerB,BrojKorakaMororB);
				}
			}
			uart_clear();

		}
    }
}

