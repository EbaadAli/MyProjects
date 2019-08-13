.text
.globl    _start

start = 0     /* starting value for the loop index; note that this is a symbol (constant), not a variable */
max = 31           /* loop exits when the index hits this number (loop condition is i<max) */
cvt = 48
d = 10

_start:
    mov     $start,%r15         /* loop index */
    mov     $0x30, %r11
    mov     $d,%r10   /*loading "10" into r10 */
loop:
    mov    %r15,%rax /*putting increment into rax because div requires dividend to be in rax */
    mov     $0,%rdx  /* div stores the quotient in rax and the remainder in rdx, this sets rdx to 0 */
    div     %r10      /*rax div 10  */
    mov     %rax,%r9  /* move the quotient to r9 */
    
    mov     %rdx,%r8  /* moving the remainder, which we needed to display 2nd digit */
    add     $cvt,%r8  /* add 48... */
    cmp     $0,%r9b
    je      print
       
  
    add     $cvt,%r9  /* add 48 to get proper ASCII char to display */
        
  
    movb    %r9b,msg+6
print:
    movb    %r8b, msg+7
    mov    $msg,%rsi
    mov    $len,%rdx /*  position to display */
    mov    $1,%rdi     
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

