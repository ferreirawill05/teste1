import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function PossuiEspecial() : ValidatorFn{
  return (control : AbstractControl) : null | ValidationErrors => {

    const validandoSenha = control.value as string

    const caracteresEspeciais = "!@#$%^&*()_+{}[]:;<>,.?~\\|'\"-=";

    for (let i = 0; i < validandoSenha.length; i++) {
      if (caracteresEspeciais.includes(validandoSenha[i])) {
        return null;
      }
    }

    return { erroCaractereEspecial: true };


  }
}
