import { TestBed } from '@angular/core/testing';

import { OrderDataStatusAdjustService } from './order-data-status-adjust.service';

describe('OrderDataStatusAdjustService', () => {
  let service: OrderDataStatusAdjustService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OrderDataStatusAdjustService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
