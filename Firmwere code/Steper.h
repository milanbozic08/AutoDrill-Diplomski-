/*
 * Steper.h
 *
 * Created: 2.8.2018 11:20:21
 *  Author: Bozic
 */ 
 #include <stdbool.h>

 enum smer {Napred=0,Nazad=1};

#ifndef STEPER_H_
#define STEPER_H_

#define Steper1A_ON PORTA |= (1<<0)
#define Steper1B_ON PORTA |= (1<<1)
#define Steper1C_ON PORTA |= (1<<2)
#define Steper1D_ON PORTA |= (1<<3)

#define Steper2A_ON PORTB |= (1<<0)
#define Steper2B_ON PORTB |= (1<<1)
#define Steper2C_ON PORTB |= (1<<2)
#define Steper2D_ON PORTB |= (1<<3)

#define Steper1A_OFF PORTA &= ~(1<<0)
#define Steper1B_OFF PORTA &= ~(1<<1)
#define Steper1C_OFF PORTA &= ~(1<<2)
#define Steper1D_OFF PORTA &= ~(1<<3)

#define Steper2A_OFF PORTB &= ~(1<<0)
#define Steper2B_OFF PORTB &= ~(1<<1)
#define Steper2C_OFF PORTB &= ~(1<<2)
#define Steper2D_OFF PORTB &= ~(1<<3)

#define Switch1IsSet (PIND & (1<<PIND4))==0
#define Switch2IsSet (PIND & (1<<PIND3))==0 

void	timer1_init();
void    timer2_init();

void InicSteper1();
void InicSteper2();

 void Steper1Run(int Speed,enum smer SMR,long int BrKoraka); //Speed u Hz
 void Steper2Run(int Speed,enum smer SMR,long int BrKoraka);

bool StepersToStart();

bool Steper1Ready();	//Ako je spreman za novu komandu vraca true;
bool Steper2Ready();

  void Steper1SmerNapred();
  void Steper2SmerNapred();

   void StepersGetOut();

   void FANSet();

   void MOTORSet();

   int Steper1Stop();
   int Steper2Stop();



#endif /* STEPER_H_ */