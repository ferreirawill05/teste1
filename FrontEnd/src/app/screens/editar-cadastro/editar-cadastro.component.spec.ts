import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditarCadastroComponent } from './editar-cadastro.component';

describe('EditarCadastroComponent', () => {
  let component: EditarCadastroComponent;
  let fixture: ComponentFixture<EditarCadastroComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditarCadastroComponent]
    });
    fixture = TestBed.createComponent(EditarCadastroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
