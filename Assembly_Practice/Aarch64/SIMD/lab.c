#include <stdio.h>
#include <stdlib.h>
#include <time.h>
int main(){

srand(-1);
   int a[1000] __attribute__((__aligned__(16)));
   int  b[1000] __attribute__((__aligned__(16)));
   int c[1000] __attribute__((__aligned__(16)));
   int sum=0;
   int i=0;


   for (i;i<1000;i++){
     a[i] = rand()%2000-1000;
     b[i] = rand()%2000-1000;
     c[i] = a[i] + b[i];
     sum += c[i];
   }

 
  printf("total sum: %d\n", sum);
}
