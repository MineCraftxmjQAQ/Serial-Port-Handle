#include "stm32f10x.h"                  // Device header
#include "Serial.h"
#include "Handle.h"
#include "Delay.h"
#include "OLED.h"
#include "Key.h"
#include "AD.h"
#include "math.h"
#include "Timer.h"

uint16_t R_CenterX, R_CenterY, L_CenterX, L_CenterY;
int16_t R_X, R_Y, L_X, L_Y;
uint8_t KeyNum, temp;

int main(void)
{
	Serial_Init();
	OLED_Init();
	AD_Init();
	Key_Init();
	Timer_Init();
	HandleCalibration(&R_CenterX, &R_CenterY, &L_CenterX, &L_CenterY);//摇杆初始化校准
	OLED_ShowString(1, 1, "R_X:");
	OLED_ShowString(2, 1, "R_Y:");
	OLED_ShowString(3, 1, "L_X:");
	OLED_ShowString(4, 1, "L_Y:");
	while (1)
	{
		R_X = HandleNumericMapping(AD_Value[1], 4096, R_CenterX, 1000);
		R_Y = HandleNumericMapping(AD_Value[0], 4096, R_CenterY, 1000);
		L_X = HandleNumericMapping(AD_Value[3], 4096, L_CenterX, 1000);
		L_Y = HandleNumericMapping(AD_Value[2], 4096, L_CenterY, 1000);
		OLED_ShowSignedNum(1, 5, R_X, 4);
		OLED_ShowSignedNum(2, 5, R_Y, 4);
		OLED_ShowSignedNum(3, 5, L_X, 4);
		OLED_ShowSignedNum(4, 5, L_Y, 4);
			KeyNum = Key_GetNum();
		if (KeyNum > 0)
		{
			temp = KeyNum;
			OLED_ShowNum(1, 12, temp, 2);
		}
		Serial_SendNumber(R_X + 1000, 4);
		Serial_SendString(" ");
		Serial_SendNumber(R_Y + 1000, 4);
		Serial_SendString(" ");
		Serial_SendNumber(L_X + 1000, 4);
		Serial_SendString(" ");
		Serial_SendNumber(L_Y + 1000, 4);
		Serial_SendString(" ");
		Serial_SendNumber(KeyNum, 2);
		Serial_SendString("\n");
	}
}
