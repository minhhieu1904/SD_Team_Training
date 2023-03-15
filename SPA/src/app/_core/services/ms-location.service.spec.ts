import { TestBed } from '@angular/core/testing';

import { MsLocationService } from './ms-location.service';

describe('MsLocationService', () => {
  let service: MsLocationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MsLocationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
