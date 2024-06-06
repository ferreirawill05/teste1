import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function ContemMaiuscula() : ValidatorFn{
  return (control : AbstractControl) : null | ValidationErrors => {

    const validandoSenha = control.value as string

    for (let i = 0; i < validandoSenha.length; i++) {
      const char = validandoSenha.charAt(i);
      if (char === char.toUpperCase() && char !== char.toLowerCase()) {
        return null;
      }
    }
    return { erroMaiuscula: true };

  }
}
