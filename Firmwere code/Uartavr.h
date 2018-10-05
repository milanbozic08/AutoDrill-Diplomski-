/*
 * UartC__.h
 *
 * Created: 9.5.2017 22:03:05
 *  Author: Bozic
 */
#include <stdbool.h>
#include <avr/io.h>
#include <string.h>
#include <stdlib.h>
#include <avr/interrupt.h>
#include <stdbool.h>


#ifndef UARTavr_H_
#define UARTavr_H_

#ifndef F_CPU
#define F_CPU 8000000UL
#endif

#ifndef BAUD
#define BAUD 38400
#endif

#define MYUBRR F_CPU/16/BAUD -1

extern volatile char Resive[];

void Uart_inic();
void uart_send_string(char *);
bool uart_full();
void uart_clear();





#endif
 /* UARTC++_H_ */