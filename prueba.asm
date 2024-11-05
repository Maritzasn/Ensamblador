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
	mov eax,4
	mov ebx,1
	mov ecx,Cadena1
	mov edx,19
	int 0x80
; Asignacion a altura
	mov eax, entero
	mov eax, 3
	mov ebx, 0
	mov ecx, buffer
	mov edx, 10
	int 0x80
	mov dword [altura], buffer
; Termina asignacion a altura
; Asignacion a altura
	mov eax, 5
	push eax
	pop eax
	mov dword [altura], eax
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
	mov eax,4
	mov ebx,1
	mov ecx,Cadena3
	mov edx,2
	int 0x80
; Else
	jmp _IF1
_Else1:
	mov eax,4
	mov ebx,1
	mov ecx,Cadena4
	mov edx,2
	int 0x80
	jmp _FinElse2
_IF1:
	jmp _Else1
_FinElse2:
	jmp _ForIncr7
_ForFin6:
	mov eax,4
	mov ebx,1
	mov ecx,Cadena5
	mov edx,1
	int 0x80
	mov eax,4
	mov ebx,1
	mov ecx,Cadena6
	mov edx, 10
	int 0x80
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
	mov eax,4
	mov ebx,1
	mov ecx,Cadena7
	mov edx,2
	int 0x80
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
	jge _DO1
	mov eax,4
	mov ebx,1
	mov ecx,Cadena8
	mov edx,1
	int 0x80
	mov eax,4
	mov ebx,1
	mov ecx,Cadena9
	mov edx, 10
	int 0x80
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
	mov eax,4
	mov ebx,1
	mov ecx,Cadena10
	mov edx,1
	int 0x80
	mov eax,[j]
	mov edi, buffer
	add edi, 9
	mov ecx, 10  
	;Conversion
	convertir: 
	xor edx, edx
	div ecx
	add dl, '0'
	dec edi
	mov [edi], dl
	test eax, eax 
	jnz convertir
	mov eax, 4
	mov ebx, 1
	mov ecx, edi
	mov edx, 10
	int 0x80
; Asignacion a j
	inc dword [j]
; Termina asignacion a j
	jmp _WhileIn1
WhileFin2:
	mov eax,4
	mov ebx,1
	mov ecx,Cadena11
	mov edx,1
	int 0x80
	mov eax,4
	mov ebx,1
	mov ecx,Cadena12
	mov edx, 10
	int 0x80
	jmp _ForIncr11
_ForFin10:
; Asignacion a i
	mov eax, 0
	push eax
	pop eax
	mov dword [i], eax
; Termina asignacion a i
; Do 2
_DO2:
	mov eax,4
	mov ebx,1
	mov ecx,Cadena13
	mov edx,2
	int 0x80
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
	jge _DO2
	mov eax,4
	mov ebx,1
	mov ecx,Cadena14
	mov edx,1
	int 0x80
	mov eax,4
	mov ebx,1
	mov ecx,Cadena15
	mov edx, 10
	int 0x80
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
	Cadena3 db "*", 0
	Cadena4 db "-", 0
	Cadena5 db "", 0
	Cadena6 db 10, 0
	Cadena7 db "-", 0
	Cadena8 db "", 0
	Cadena9 db 10, 0
	Cadena10 db "", 0
	Cadena11 db "", 0
	Cadena12 db 10, 0
	Cadena13 db "-", 0
	Cadena14 db "", 0
	Cadena15 db 10, 0
	entero db "%d",0
	caracter db "%c",0
	flotante db "%f",0
	buffer db 10, 0

segment .bss
	Cadena2 resb 16 
