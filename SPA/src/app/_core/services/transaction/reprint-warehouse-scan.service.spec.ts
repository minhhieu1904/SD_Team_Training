import { TestBed } from '@angular/core/testing';

import { ReprintWarehouseScanService } from './reprint-warehouse-scan.service';

describe('ReprintWarehouseScanService', () => {
  let service: ReprintWarehouseScanService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ReprintWarehouseScanService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
