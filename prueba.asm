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
; For 0
; Asignacion a x
	mov eax, 0
	push eax
	pop eax
	mov dword [x], eax
; Termina asignacion a x
_ForCond1:
	mov eax, [x]
	push eax
	mov eax, 10
	push eax
	pop ebx
	pop eax
	cmp eax, ebx
	je ForFin2
	jmp _ForIni0
_ForIncr3:
; Asignacion a x
	inc dword [x]
; Termina asignacion a x
	jmp _ForCond1
_ForIni0:
; Asignacion a y
	mov eax, [x]
	push eax
	mov eax, 2
	push eax
	pop ebx
	pop eax
	mul ebx
	push eax
	pop eax
	mov dword [y], eax
; Termina asignacion a y
	jmp _ForIncr3
ForFin2:
	add esp, 4

	mov eax, 1
	xor ebx, ebx
	int 0x80

segment .data
	x dd 0
	y dd 0
