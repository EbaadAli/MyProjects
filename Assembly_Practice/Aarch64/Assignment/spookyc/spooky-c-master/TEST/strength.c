#include <stdio.h>

int main(){
double mult[1000];
double add[1000];

int i,j;
for (i=0; i<1000; i++){
    mult[i] = 2 * i;
    add[i] = i + i; 

    if (mult[i] != add[i])
	    printf("value %d is false\n", i);
    else
	    printf("value %d is true\n", i);
}






}
