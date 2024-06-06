import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function ContemNumero() : ValidatorFn{
  return (control : AbstractControl) : null | ValidationErrors => {

    const validandoSenha = control.value as string

    for (let i = 0; i < validandoSenha.length; i++) {
      const char = validandoSenha.charAt(i);
      if (parseInt(char)) {
        return null;
      }
    }
    return { erroNumero: true };

  }
}
