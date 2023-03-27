/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MsPackageService } from './ms-package.service';

describe('Service: MsPackage', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MsPackageService]
    });
  });

  it('should ...', inject([MsPackageService], (service: MsPackageService) => {
    expect(service).toBeTruthy();
  }));
});
