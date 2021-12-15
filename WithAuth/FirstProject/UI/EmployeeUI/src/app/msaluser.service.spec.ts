import { TestBed } from '@angular/core/testing';

import { MsaluserService } from './msaluser.service';

describe('MsaluserService', () => {
  let service: MsaluserService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MsaluserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
