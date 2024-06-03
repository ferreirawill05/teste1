import { TestBed } from '@angular/core/testing';

import { TelefoneServiceService } from './telefone-service.service';

describe('TelefoneServiceService', () => {
  let service: TelefoneServiceService;

  beforeEach(() => { 
    TestBed.configureTestingModule({});
    service = TestBed.inject(TelefoneServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
