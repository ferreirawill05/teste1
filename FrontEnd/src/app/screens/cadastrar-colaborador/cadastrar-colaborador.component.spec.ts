import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastrarColaboradorComponent } from './cadastrar-colaborador.component';

describe('CadastrarColaboradorComponent', () => {
  let component: CadastrarColaboradorComponent;
  let fixture: ComponentFixture<CadastrarColaboradorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CadastrarColaboradorComponent]
    });
    fixture = TestBed.createComponent(CadastrarColaboradorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
