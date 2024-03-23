#ifndef __HANDLE_H
#define __HANDLE_H

void HandleCalibration(uint16_t *R_CenterX, uint16_t *R_CenterY, uint16_t *L_CenterX, uint16_t *L_CenterY);
int16_t HandleNumericMapping(uint16_t InputNum, uint16_t InputRange, uint16_t OutputRange, uint16_t OutputCentralValue);

#endif
