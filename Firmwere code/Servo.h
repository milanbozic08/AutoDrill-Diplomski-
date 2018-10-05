/*
 * Servo.h
 *
 * Created: 2.8.2018 11:08:58
 *  Author: Bozic
 */ 
 #include <stdbool.h>

#ifndef SERVO_H_
#define SERVO_H_

#define F50Hz 156*8
#define ServoSpeed 4000
#define ServoPinON PORTA|=(1<<6)
#define ServoPinOFF PORTA&=~(1<<6)

#define MotorON  PORTD|=(1<<7)
#define MotorOFF PORTD&=~(1<<7)

#define FanON  PORTC|=(1<<7)
#define FanOFF PORTC&=~(1<<7)

void inicMotorAndFan();
void inicServo();
void ServoSet(int in);
bool ServoReady();

void SetServoPauza(int x);
void Pauza_ms(int k);






#endif /* SERVO_H_ */