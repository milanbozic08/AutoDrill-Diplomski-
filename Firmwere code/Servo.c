//#define F_CPU 8000000UL
#include "Servo.h"
#include <avr/interrupt.h>
#include <stdbool.h>
#include <util/delay.h>

//Od 35 do 50 Taj opseg koristiti !!!

volatile int Timer0Value=0,Timer0Value1=0,Timer0Value2=0,Timer0Value3=0;
volatile int ServoValue=48,ServoValueTMP=0,MotorPWM=0,FanPWM=0,ServoPauza=5000; // 40 - 48


void inicMotorAndFan()
{
	DDRD |=(1<<7); // GL motor
	DDRC |=(1<<7); // FAN
}

void timer0_init()  //1/(16Mhz/8)*255=0.000128 = 128uS
{
	// set up timer prescaling 8
	TCCR0 |= (1 << CS00);

	// initialize counter
	TCNT0 = 0;
	
	// enable overflow interrupt
	TIMSK |= (1 << TOIE0);
}

void ServoSet(int in)
{
	ServoValueTMP=in;
}

void FANSet(int PWM)
{
	FanPWM=PWM;

	if(FanPWM>99) FanPWM=99;
	if(FanPWM<1) FanPWM=1;
}

void MOTORSet(int PWM)
{
	MotorPWM=PWM;

	if(MotorPWM>99) MotorPWM=99;
	if(MotorPWM<1) MotorPWM=1;
}

void inicServo()
{
	ServoPinON;
	DDRA |=(1<<6); // Servo izlazni
	timer0_init();
}

bool ServoReady()
{
	bool Redy=false;
	if(ServoValue==ServoValueTMP) 
	{
		Redy=true;
	}
	return Redy;
}

ISR(TIMER0_OVF_vect)  // Aktivira se svakih 128uS		// 1 ms je 8 interapta
{

	Timer0Value++;
	Timer0Value1++;
	Timer0Value2++;
	Timer0Value3++;

	if(Timer0Value2>MotorPWM)
	{
		MotorOFF;
	}
	if(Timer0Value2>100)
	{
		MotorON;
		Timer0Value2=0;
	}

	if(Timer0Value3>FanPWM)
	{
		FanOFF;
	}
	if(Timer0Value3>100)
	{
		FanON;
		Timer0Value3=0;
	}

	if(Timer0Value>ServoValue)
	{
		ServoPinOFF;
	}
	if(Timer0Value>F50Hz)
	{
		ServoPinON;
		Timer0Value=0;
	}

	if(Timer0Value1>ServoSpeed)
	{
		Timer0Value1=0;

		if(ServoValue!=ServoValueTMP)
		{
			if(ServoValue>ServoValueTMP)
			{
				ServoValue--;
			}
			else ServoValue++;
		}
	}

}