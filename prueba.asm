; Autor: Camila Patricia Mata Gallegos
; Analisis léxico
; Analizador Sintactico

extern fflush
extern printf
extern scanf
extern stdout

segment .text
	global _main

_main:
; Asignacion a x
	mov eax, 3
	push eax
	mov eax, 5
	push eax
	pop ebx
	pop eax
	add eax, ebx
	push eax
	mov eax, 8
	push eax
	pop ebx
	pop eax
	mul ebx
	push eax
	mov eax, 10
	push eax
	mov eax, 4
	push eax
	pop ebx
	pop eax
	sub eax, ebx
	push eax
	mov eax, 2
	push eax
	pop ebx
	pop eax
	div ebx
	push eax
	pop ebx
	pop eax
	sub eax, ebx
	push eax
	pop eax
	mov dword +[x], eax
; Termina asignacion a x
; Asignacion a x
	inc dword [x]
; Termina asignacion a x
; If1
	mov eax, x
	push eax
	mov eax, 62
	push eax
	pop eax
	pop ebx
	cmp eax, ebx
	jne _IF1
; Asignacion a x
	mov eax, 0
	push eax
	pop eax
	mov dword +[x], eax
; Termina asignacion a x
; If2
	mov eax, x
	push eax
	mov eax, 0
	push eax
	pop eax
	pop ebx
	cmp eax, ebx
	je _IF2
; Asignacion a x
	mov eax, 1
	push eax
	pop eax
	mov dword +[x], eax
; Termina asignacion a x
_IF2:
_IF1:
; Asignacion a x
	mov eax, 0
	push eax
	pop eax
	mov dword +[x], eax
; Termina asignacion a x
; While 1
_WhileIn1:
	mov eax, x
	push eax
	mov eax, 10
	push eax
	pop eax
	pop ebx
	cmp eax, ebx
	je WhileFin2
; Asignacion a x
	inc dword [x]
; Termina asignacion a x
	jmp _WhileIn1
WhileFin2:
	add esp, 4

	mov eax, 1
	xor ebx, ebx
	int 0x80

segment .data
	x dd 0
	y dd 0
