; Autor: Camila Patricia Mata Gallegos 
; Maritza Belen Nuñez 
; Berenice Hernandez Juarez
; Analisis léxico
; Analizador Sintactico

extern fflush
extern printf
extern scanf
extern stdout

segment .text
	global main

main:
; Asignacion a altura
	mov eax, entero
	mov ebx, altura
	push ebx
	push eax
	call scanf
	add esp,8
; Termina asignacion a altura
; Asignacion a k
	mov eax, 1
	push eax
	pop eax
	mov dword [k], eax
; Termina asignacion a k
; For 0
; Asignacion a i
	mov eax, 1
	push eax
	pop eax
	mov dword [i], eax
; Termina asignacion a i
_ForCond1:
	mov eax, [k]
	push eax
	mov eax, [altura]
	push eax
	pop ebx
	pop eax
	cmp eax, ebx
	jg ForFin2
	jmp _ForIni0
_ForIncr3:
; Asignacion a k
	inc dword [k]
; Termina asignacion a k
	jmp _ForCond1
_ForIni0:
; For 4
; Asignacion a j
	mov eax, 1
	push eax
	pop eax
	mov dword [j], eax
; Termina asignacion a j
_ForCond5:
	mov eax, [j]
	push eax
	mov eax, [k]
	push eax
	pop ebx
	pop eax
	cmp eax, ebx
	jg ForFin6
	jmp _ForIni4
_ForIncr7:
; Asignacion a j
	inc dword [j]
; Termina asignacion a j
	jmp _ForCond5
_ForIni4:
; If1
	mov eax, [j]
	push eax
	mov eax, 2
	push eax
	pop ebx
	pop eax
	xor edx, edx
	div ebx
	push edx
	mov eax, 0
	push eax
	pop ebx
	pop eax
	cmp eax, ebx
	jne _IF1
	push dword Cadena1
	call printf
	add esp, 4
; Else
	jmp _IF1
_Else1:
	push dword Cadena2
	call printf
	add esp, 4
	jmp _FinElse2
_IF1:
	jmp _Else1
_FinElse2:
	jmp _ForIncr7
ForFin6:
	push dword Cadena3
	call printf
	add esp, 4
	push dword Cadena4
	call printf
	add esp, 4
	jmp _ForIncr3
ForFin2:
	add esp, 4

	mov eax, 1
	xor ebx, ebx
	int 0x80

segment .data
	altura dd 0
	i dd 0
	k dd 0
	j dw 0 
	Cadena1 db '*', 0
	Cadena2 db '-', 0
	Cadena3 db '', 0
	Cadena4 db 10, 0
	entero db "%d",0
	caracter db "%c",0
	flotante db "%f",0
