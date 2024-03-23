#include "stm32f10x.h"                  // Device header
#include "Delay.h"
#include "OLED.h"
#include "AD.h"
#include "math.h"

void HandleCalibration(uint16_t *R_CenterX, uint16_t *R_CenterY, uint16_t *L_CenterX, uint16_t *L_CenterY)
{
	uint8_t i; 
	OLED_ShowString(1, 1, "Calibration");
	OLED_ShowString(2, 1, "Start!");
	Delay_ms(1000);
	*R_CenterX = 0;
	*R_CenterY = 0;
	*L_CenterX = 0;
	*L_CenterY = 0;
	for(i = 0; i < 30; i++)
	{
		*R_CenterX += AD_GetValue(ADC_Channel_1);
		*R_CenterY += AD_GetValue(ADC_Channel_0);
		*L_CenterX += AD_GetValue(ADC_Channel_3);
		*L_CenterY += AD_GetValue(ADC_Channel_2);
		Delay_ms(100);
	}
	*R_CenterX = floor(*R_CenterX / 30);
	*R_CenterY = floor(*R_CenterY / 30);
	*L_CenterX = floor(*L_CenterX / 30);
	*L_CenterY = floor(*L_CenterY / 30);
	OLED_ShowString(2, 1, "Finfish!");
	Delay_ms(1000);
	OLED_Clear();
	OLED_ShowString(1, 1, "R_CenterX:");
	OLED_ShowString(2, 1, "R_CenterY:");
	OLED_ShowString(3, 1, "L_CenterX:");
	OLED_ShowString(4, 1, "L_CenterY:");
	OLED_ShowNum(1, 11, *R_CenterX, 4);
	OLED_ShowNum(2, 11, *R_CenterY, 4);
	OLED_ShowNum(3, 11, *L_CenterX, 4);
	OLED_ShowNum(4, 11, *L_CenterY, 4);
	Delay_ms(2000);
	OLED_Clear();
}

int16_t HandleNumericMapping(uint16_t InputNum, uint16_t InputRange, uint16_t InputCentralValue, uint16_t OutputRange)
{
	if(InputNum < InputCentralValue)
	{
		if(InputCentralValue - InputNum < 20)
			return 0;
		else
			return OutputRange * (InputCentralValue - InputNum) / InputCentralValue;
	}
	else
	{
		if(InputNum - InputCentralValue < 20)
			return 0;
		else
			return -OutputRange * (InputNum - InputCentralValue) / (InputRange - InputCentralValue);
	}
}
