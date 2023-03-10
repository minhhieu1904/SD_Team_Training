/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { WarehouseDataService } from './warehouse-data.service';

describe('Service: WarehouseData', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WarehouseDataService]
    });
  });

  it('should ...', inject([WarehouseDataService], (service: WarehouseDataService) => {
    expect(service).toBeTruthy();
  }));
});
