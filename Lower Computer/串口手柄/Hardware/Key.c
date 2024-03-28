#include "stm32f10x.h"                  // Device header
#include "Delay.h"
#include "Timer.h"

#define TIMEOUT_DURATION 3000//短按长按阈值控制

void Key_Init(void)
{
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA, ENABLE);
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOB, ENABLE);
	
	GPIO_InitTypeDef GPIO_InitStructure;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IPU;
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_4 | GPIO_Pin_5;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOA, &GPIO_InitStructure);
	
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IPD;
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_0 | GPIO_Pin_1 | GPIO_Pin_10 | GPIO_Pin_11;
	GPIO_Init(GPIOB, &GPIO_InitStructure);
	
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_Out_PP;
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_12 | GPIO_Pin_13 | GPIO_Pin_14 | GPIO_Pin_15;
	GPIO_Init(GPIOB, &GPIO_InitStructure);
}

uint8_t Key_GetNum(void)
{
	uint8_t KeyNum = 0;
	if (GPIO_ReadInputDataBit(GPIOA, GPIO_Pin_4) == 0)
	{
		Delay_ms(20);
		Key_Scan_Open();
		while (GPIO_ReadInputDataBit(GPIOA, GPIO_Pin_4) == 0){};
		if (Key_Scan_Close() < TIMEOUT_DURATION)
			KeyNum = 17;
		else
			KeyNum = 49;
	}
	if (GPIO_ReadInputDataBit(GPIOA, GPIO_Pin_5) == 0)
	{
		Delay_ms(20);
		Key_Scan_Open();
		while (GPIO_ReadInputDataBit(GPIOA, GPIO_Pin_5) == 0){};
		if (Key_Scan_Close() < TIMEOUT_DURATION)
			KeyNum = 18;
		else
			KeyNum = 50;
	}
	
	GPIO_WriteBit(GPIOB, GPIO_Pin_12, Bit_SET);
	GPIO_WriteBit(GPIOB, GPIO_Pin_13, Bit_RESET);
	GPIO_WriteBit(GPIOB, GPIO_Pin_14, Bit_RESET);
	GPIO_WriteBit(GPIOB, GPIO_Pin_15, Bit_RESET);
	if (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_0) == 1)
	{
		Delay_ms(20);
		Key_Scan_Open();
		while (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_0) == 1){};
		if (Key_Scan_Close() < TIMEOUT_DURATION)
			KeyNum = 4;
		else
			KeyNum = 36;
	}
	if (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_1) == 1)
	{
		Delay_ms(20);
		Key_Scan_Open();
		while (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_1) == 1){};
		if (Key_Scan_Close() < TIMEOUT_DURATION)
			KeyNum = 3;
		else
			KeyNum = 35;
	}
	if (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_10) == 1)
	{
		Delay_ms(20);
		Key_Scan_Open();
		while (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_10) == 1){};
		if (Key_Scan_Close() < TIMEOUT_DURATION)
			KeyNum = 2;
		else
			KeyNum = 34;
	}
	if (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_11) == 1)
	{
		Delay_ms(20);
		Key_Scan_Open();
		while (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_11) == 1){};
		if (Key_Scan_Close() < TIMEOUT_DURATION)
			KeyNum = 1;
		else
			KeyNum = 33;
	}
	
	GPIO_WriteBit(GPIOB, GPIO_Pin_12, Bit_RESET);
	GPIO_WriteBit(GPIOB, GPIO_Pin_13, Bit_SET);
	GPIO_WriteBit(GPIOB, GPIO_Pin_14, Bit_RESET);
	GPIO_WriteBit(GPIOB, GPIO_Pin_15, Bit_RESET);
	if (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_0) == 1)
	{
		Delay_ms(20);
		Key_Scan_Open();
		while (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_0) == 1){};
		if (Key_Scan_Close() < TIMEOUT_DURATION)
			KeyNum = 8;
		else
			KeyNum = 40;
	}
	if (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_1) == 1)
	{
		Delay_ms(20);
		Key_Scan_Open();
		while (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_1) == 1){};
		if (Key_Scan_Close() < TIMEOUT_DURATION)
			KeyNum = 7;
		else
			KeyNum = 39;
	}
	if (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_10) == 1)
	{
		Delay_ms(20);
		Key_Scan_Open();
		while (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_10) == 1){};
		if (Key_Scan_Close() < TIMEOUT_DURATION)
			KeyNum = 6;
		else
			KeyNum = 38;
	}
	if (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_11) == 1)
	{
		Delay_ms(20);
		Key_Scan_Open();
		while (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_11) == 1){};
		if (Key_Scan_Close() < TIMEOUT_DURATION)
			KeyNum = 5;
		else
			KeyNum = 37;
	}
	
	GPIO_WriteBit(GPIOB, GPIO_Pin_12, Bit_RESET);
	GPIO_WriteBit(GPIOB, GPIO_Pin_13, Bit_RESET);
	GPIO_WriteBit(GPIOB, GPIO_Pin_14, Bit_SET);
	GPIO_WriteBit(GPIOB, GPIO_Pin_15, Bit_RESET);
	if (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_0) == 1)
	{
		Delay_ms(20);
		Key_Scan_Open();
		while (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_0) == 1){};
		if (Key_Scan_Close() < TIMEOUT_DURATION)
			KeyNum = 12;
		else
			KeyNum = 44;
	}
	if (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_1) == 1)
	{
		Delay_ms(20);
		Key_Scan_Open();
		while (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_1) == 1){};
		if (Key_Scan_Close() < TIMEOUT_DURATION)
			KeyNum = 11;
		else
			KeyNum = 43;
	}
	if (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_10) == 1)
	{
		Delay_ms(20);
		Key_Scan_Open();
		while (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_10) == 1){};
		if (Key_Scan_Close() < TIMEOUT_DURATION)
			KeyNum = 10;
		else
			KeyNum = 42;
	}
	if (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_11) == 1)
	{
		Delay_ms(20);
		Key_Scan_Open();
		while (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_11) == 1){};
		if (Key_Scan_Close() < TIMEOUT_DURATION)
			KeyNum = 9;
		else
			KeyNum = 41;
	}
	
	GPIO_WriteBit(GPIOB, GPIO_Pin_12, Bit_RESET);
	GPIO_WriteBit(GPIOB, GPIO_Pin_13, Bit_RESET);
	GPIO_WriteBit(GPIOB, GPIO_Pin_14, Bit_RESET);
	GPIO_WriteBit(GPIOB, GPIO_Pin_15, Bit_SET);
	if (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_0) == 1)
	{
		Delay_ms(20);
		Key_Scan_Open();
		while (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_0) == 1){};
		if (Key_Scan_Close() < TIMEOUT_DURATION)
			KeyNum = 16;
		else
			KeyNum = 48;
	}
	if (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_1) == 1)
	{
		Delay_ms(20);
		Key_Scan_Open();
		while (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_1) == 1){};
		if (Key_Scan_Close() < TIMEOUT_DURATION)
			KeyNum = 15;
		else
			KeyNum = 47;
	}
	if (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_10) == 1)
	{
		Delay_ms(20);
		Key_Scan_Open();
		while (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_10) == 1){};
		if (Key_Scan_Close() < TIMEOUT_DURATION)
			KeyNum = 14;
		else
			KeyNum = 46;
	}
	if (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_11) == 1)
	{
		Delay_ms(20);
		Key_Scan_Open();
		while (GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_11) == 1){};
		if (Key_Scan_Close() < TIMEOUT_DURATION)
			KeyNum = 13;
		else
			KeyNum = 45;
	}
	return KeyNum;
}
