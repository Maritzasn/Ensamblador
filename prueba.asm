; Autor: Camila Patricia Mata Gallegos
; Analisis léxico
; Analizador Sintactico

extern fflush
extern printf
extern scanf
extern stdout

segment .text
	global main

main:
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
	mov dword [x], eax
; Termina asignacion a x
; Asignacion a x
	inc dword [x]
; Termina asignacion a x
; If1
	mov eax, [x]
	push eax
	mov eax, 62
	push eax
	pop ebx
	pop eax
	cmp eax, ebx
	je _IF1
; Asignacion a x
	mov eax, 0
	push eax
	pop eax
	mov dword [x], eax
; Termina asignacion a x
	jmp _IF1
_Else1:
; Asignacion a x
	mov eax, 2
	push eax
	pop eax
	mov dword [x], eax
; Termina asignacion a x
	jmp _FinElse2
_IF1:
	jmp _Else1
_FinElse2:
	add esp, 4

	mov eax, 1
	xor ebx, ebx
	int 0x80

segment .data
	x dd 0
	y dd 0
