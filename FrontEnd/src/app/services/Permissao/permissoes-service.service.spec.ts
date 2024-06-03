import { TestBed } from '@angular/core/testing';

import { PermissoesServiceService } from './permissoes-service.service';

describe('PermissoesServiceService', () => {
  let service: PermissoesServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PermissoesServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
