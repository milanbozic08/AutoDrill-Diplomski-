/*
 * UartAVRc.c
 *
 * Created: 23.5.2017 14:11:19
 *  Author: Bozic

 */ 


#include "Uartavr.h"
#include <avr/interrupt.h>


volatile char Resive[100];
volatile bool ResivedOK=false;
volatile int brojcanik=-1;

void Uart_inic()
{
	UBRRH =(MYUBRR)>>8;
	UBRRL = MYUBRR;
	
	UCSRB |= (1 << RXEN) | (1 << TXEN);      // Enable receiver and transmitter
	UCSRB |= (1 << RXCIE);                   // Enable the receiver interrupt
	UCSRC |= (1 << URSEL) |(1 << UCSZ1) | (1 << UCSZ0);    // Set frame: 8data, 1 stp
	
	
	for(int k=0;k<100;k++)
	{
		Resive[k]='\0';
	}	  
}

void uart_send_string(char *Str)
{
	int x=strlen(Str),n=0;
	for(n=0;n<x;n++)
	{
		while ( !(UCSRA & (1 << UDRE)) );   // Wait until buffer is empty

		UDR = *Str;                     // Send the data to the TX buffer
		Str++;
	}
}

bool uart_full()	// ako je false onda nista nije stiglo ...
{
	return ResivedOK;
}

void uart_clear()
{
	ResivedOK=false;
	
	for(int k=0;k<100;k++)
	{
		Resive[k]='\0';
	}

	brojcanik=-1;
}

ISR (USART_RXC_vect)
{
						
		brojcanik++;										
		if(brojcanik>99) brojcanik=99;											//ne daj prepunjavanje bafera
	
		Resive[brojcanik]=UDR;
		
		if(brojcanik>2)
		if(Resive[brojcanik]=='/' && Resive[brojcanik-1]=='/')
		{
			for(int x=(brojcanik-1);x<100;x++)
			Resive[x]='\0';

			brojcanik=-1;

			ResivedOK=true;
		}	
}