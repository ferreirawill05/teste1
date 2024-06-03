import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoubleMsgComponent } from './double-msg.component';

describe('DoubleMsgComponent', () => {
  let component: DoubleMsgComponent;
  let fixture: ComponentFixture<DoubleMsgComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DoubleMsgComponent]
    });
    fixture = TestBed.createComponent(DoubleMsgComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
