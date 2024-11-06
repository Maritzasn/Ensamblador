; Autor: Camila Patricia Mata Gallegos 
; Maritza Belen Nuñez 
; Berenice Hernandez Juarez
; Analisis léxico
; Analizador Sintactico

extern fflush
extern stdout

segment .text
extern printf
extern scanf
	global _start

_start:
	mov eax, 10
	push eax
	pop eax
	mov dword [y], eax
	mov eax, 2
	push eax
	pop eax
	mov dword [z], eax
; Asignacion a c
	mov eax, 100
	push eax
	mov eax, 200
	push eax
	pop ebx
	pop eax
	add eax, ebx
	push eax
	pop eax
	mov dword [c], eax
; Termina asignacion a c
	push Cadena1
	call printf
	add esp, 4
; Asignacion a altura
	push dword altura
	push dword entero
	call scanf
	add esp, 8
; Termina asignacion a altura
	mov eax, 3
	push eax
	mov eax, [altura]
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
; Asignacion a x
	dec dword [x]
; Termina asignacion a x
; Asignacion a x
	mov eax, [altura]
	push eax
	mov eax, 8
	push eax
	pop ebx
	pop eax
	mul ebx
	push eax
	pop eax
	add dword [x], eax
; Termina asignacion a x
; Asignacion a x
	mov eax, 2
	push eax
	pop ebx
	mov eax, dword [x]
	mul ebx
	mov dword [x], eax
; Termina asignacion a x
	mov eax, 1
	push eax
	pop eax
	mov dword [k], eax
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
	jg _ForFin2
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
	jg _ForFin6
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
	push Cadena2
	call printf
	add esp, 4
; Else
	jmp _FinElse2
_Else1:
	push Cadena3
	call printf
	add esp, 4
	jmp _FinElse2
_IF1:
	jmp _Else1
_FinElse2:
	jmp _ForIncr7
_ForFin6:
	push Cadena4
	call printf
	add esp, 4
	push Cadena5
	call printf
	add esp, 4
	jmp _ForIncr3
_ForFin2:
; Asignacion a i
	mov eax, 0
	push eax
	pop eax
	mov dword [i], eax
; Termina asignacion a i
; Do 1
_DO1:
	push Cadena6
	call printf
	add esp, 4
; Asignacion a i
	inc dword [i]
; Termina asignacion a i
	mov eax, [i]
	push eax
	mov eax, [altura]
	push eax
	mov eax, 2
	push eax
	pop ebx
	pop eax
	mul ebx
	push eax
	pop ebx
	pop eax
	cmp eax, ebx
	jge _DOFIN2
	jmp _DO1
_DOFIN2:
	push Cadena7
	call printf
	add esp, 4
	push Cadena8
	call printf
	add esp, 4
; For 8
; Asignacion a i
	mov eax, 1
	push eax
	pop eax
	mov dword [i], eax
; Termina asignacion a i
_ForCond9:
	mov eax, [i]
	push eax
	mov eax, [altura]
	push eax
	pop ebx
	pop eax
	cmp eax, ebx
	jg _ForFin10
	jmp _ForIni8
_ForIncr11:
; Asignacion a i
	inc dword [i]
; Termina asignacion a i
	jmp _ForCond9
_ForIni8:
; Asignacion a j
	mov eax, 1
	push eax
	pop eax
	mov dword [j], eax
; Termina asignacion a j
; While 1
_WhileIn1:
	mov eax, [j]
	push eax
	mov eax, [i]
	push eax
	pop ebx
	pop eax
	cmp eax, ebx
	jg WhileFin2
	push Cadena9
	call printf
	add esp, 4
	mov edi, entero
	mov esi, [j]
	push esi
	push edi
	call printf
; Asignacion a j
	inc dword [j]
; Termina asignacion a j
	jmp _WhileIn1
WhileFin2:
	push Cadena10
	call printf
	add esp, 4
	push Cadena11
	call printf
	add esp, 4
	jmp _ForIncr11
_ForFin10:
; Asignacion a i
	mov eax, 0
	push eax
	pop eax
	mov dword [i], eax
; Termina asignacion a i
; Do 3
_DO3:
	push Cadena12
	call printf
	add esp, 4
; Asignacion a i
	inc dword [i]
; Termina asignacion a i
	mov eax, [i]
	push eax
	mov eax, [altura]
	push eax
	mov eax, 2
	push eax
	pop ebx
	pop eax
	mul ebx
	push eax
	pop ebx
	pop eax
	cmp eax, ebx
	jge _DOFIN4
	jmp _DO3
_DOFIN4:
	push Cadena13
	call printf
	add esp, 4
	push Cadena14
	call printf
	add esp, 4
	add esp, 4

	mov eax, 1
	xor ebx, ebx
	int 0x80

segment .data
	altura dd 0
	i dd 0
	j dd 0
	y dw 0 
	z dw 0 
	c db 0
	x dw 0 
	k dd 0
	Cadena1 db "Valor de altura = ", 0
	Cadena2 db "*", 0
	Cadena3 db "-", 0
	Cadena4 db "", 0
	Cadena5 db 10, 0
	Cadena6 db "-", 0
	Cadena7 db "", 0
	Cadena8 db 10, 0
	Cadena9 db "", 0
	Cadena10 db "", 0
	Cadena11 db 10, 0
	Cadena12 db "-", 0
	Cadena13 db "", 0
	Cadena14 db 10, 0
	entero db "%d",0
	caracter db "%c",0
	flotante db "%f",0
	cadena db "%s",0
	buffer db 10, 0

segment .bss
