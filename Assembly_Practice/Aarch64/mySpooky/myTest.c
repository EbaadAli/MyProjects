#include <stdio.h>
#include <inttypes.h>
#include "spooky-c.h"
#define BUFF (512)

int main(){

uint8_t buf[BUFF];
uint8_t got[BUFF];

int i;



for (i=0;i<BUFF;++i){
	buf[i] = i;
        got[i] = spooky_hash128(buf, i, 0, 0);

	printf("got: %d\n", got[i]);
}

}
