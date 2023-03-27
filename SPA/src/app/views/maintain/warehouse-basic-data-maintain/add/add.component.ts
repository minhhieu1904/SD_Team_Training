import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { LocationParam } from '@models/maintain/warehouse-basic-data-maintenance'

import { MsLocationService } from '@services/main/ms-location.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {
  msLocation: LocationParam = <LocationParam>{
    storeH: '',
    locationName: ''
  }
  constructor(
    private service: MsLocationService,
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  backList() {
    this.router.navigate(['maintain/warehouse-basic-data-maintain']);
  }

  save() {
    this.service.addNew(this.msLocation).subscribe({
      next: result => {
        if (result.isSuccess)
          alert('Them thanh cong')
      },
      error: error => {
        alert('Them that bai')
      },
      complete: () => {
        this.msLocation.storeH = "";
        this.msLocation.locationName = ""
      }
    })
  }

  saveAndNext() {
    this.service.addNew(this.msLocation).subscribe({
      next: result => {
        if (result.isSuccess)
          alert('Them thanh cong')
      },
      error: () => {
        alert('Them that bai')
      },
      complete: () => {
        this.backList();
      }
    })
  }
}
