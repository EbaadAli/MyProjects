.text
.globl    _start

start = 0                       /* starting value for the loop index; note that this is a symbol (constant), not a variable */
max = 10                        /* loop exits when the index hits this number (loop condition is i<max) */
cvt = 48

_start:
    mov     $start,%r15         /* loop index */

loop:
    /* ... body of the loop ... do something useful here ... */
    mov    $len,%rdx  /* stores length of string in rdx */
    mov    $msg,%rsi   /* stores the actual string in rsi */
    mov    $cvt,%r14   /* storing the value 48 in r14 to display appropiate ascii value */
    mov    %r15,%r13   /* move value in r15 to r13   */
    add    %r13,%r14   /*  adding 48 to r13 */
    movb   %r14b,msg+6  /* since were storing a single char, use movb to write 1 byte, and store 6 characters to the right of our msg but only use the lower portion of that register   */ 
    mov    $1,%rdi      /*  store  status in rdi  and rax  */
    mov    $1,%rax
   
    syscall

    inc     %r15                /* increment index */
    cmp     $max,%r15           /* see if we're done */
    jne     loop                /* loop if we're not */

    mov     $0,%rdi             /* exit status */
    mov     $60,%rax            /* syscall sys_exit */
    syscall


.section .data

msg:     .ascii     "Loop:      \n"
	 len = . - msg

