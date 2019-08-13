.text
.globl    _start
start = 0 	/* starting value for the loop index; note that this is a symbol (constant), not a variable */
max = 10		/* loop exits when the index hits this number (loop condition is i<max) */
ten = 10

_start:
        mov     x19, start  /* load start into x19 */
        mov     x22, ten   /* load 10 into x22 */
		
loop:
        adr     x1, msg       /* load address label of msg into x1 */
        mov     x2, len       /* load length of msg in x2 */
        mov     x0, 1         /* stdout */ 
        mov     x8, 64        
        svc     0          
        adr     x23, msg   
        add     x19, x19, 1      /* loads x19 with x19 + 1  ( the increment ) */ 
        udiv    x20, x19, x22    /* unsigned divide the increment by ten  and load into x20 */
        msub    x21, x22, x20, x19 /* x21 = x19 - (x22 * x20)   this formula helps us calculate the remainder */ 
        cmp     x20, 0   /* see if its 0, because we want to supress leading 0's */
        beq     skip     /* if yes, print second value only */
        add     x20, x20, '0'   /* cvt to ascii */
        strb    w20, [x23,6]  /* stores a single byte to the address pointed by (x23 + (0 * size ) */
skip:
/* 2nd digit */
        add     x21, x21, '0'
        strb    w21, [x23,7]
        
/* loop check */
        cmp     x19, max
        bne     loop
        mov     x0, 0
        mov     x8, 93
        svc     0
.data
        msg: .ascii  "Loop:  0\n"
        len = . - msg
