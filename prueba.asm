; Autor: Camila Patricia Mata Gallegos
; Analisis léxico
; Analizador Sintactico
; Analizador Semantico

extern fflush
extern printf
extern scanf
extern stdout

segment .text
	global _main

_main:
; Asignacion a x
	mov ax, 3
	push ax
	mov ax, 5
	push ax
	pop bx
	pop ax
	add ax, bx
	push ax
	mov ax, 8
	push ax
	pop bx
	pop ax
	mul bx
	push ax
	mov ax, 10
	push ax
	mov ax, 4
	push ax
	pop bx
	pop ax
	sub ax, bx
	push ax
	mov ax, 2
	push ax
	pop bx
	pop ax
	div bx
	push ax
	pop bx
	pop ax
	sub ax, bx
	push ax
	pop ax
	mov x, ax
; Termina asignacion a x
; Asignacion a x
	inc x
; Termina asignacion a x
; If1
	mov ax, x
	push ax
	mov ax, 62
	push ax
	pop ax
	pop bx
	cmp ax, bx
	jne _IF1
; Asignacion a x
	mov ax, 0
	push ax
	pop ax
	mov x, ax
; Termina asignacion a x
; If2
	mov ax, x
	push ax
	mov ax, 0
	push ax
	pop ax
	pop bx
	cmp ax, bx
	je _IF2
; Asignacion a x
	mov ax, 1
	push ax
	pop ax
	mov x, ax
; Termina asignacion a x
_IF2:
_IF1:
; Asignacion a y
	mov ax, 0
	push ax
	pop ax
	mov y, ax
; Termina asignacion a y
	add esp, 4

	mov eax, 1
	xor ebx, ebx
	int 0x80

segment .data
	x db 0
	y db 0
