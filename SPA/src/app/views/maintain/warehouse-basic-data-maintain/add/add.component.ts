import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { MSLocation } from '@models/mS_Location';
import { MsLocationService } from '@services/ms-location.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {
  msLocation: MSLocation = <MSLocation>{
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
        if(result.isSuccess)
        alert ('Them thanh cong')
      },
      error: error => {
        alert ('Them that bai')
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
        if(result.isSuccess)
        alert ('Them thanh cong')
      },
      error: () => {
        alert ('Them that bai')
      },
      complete: () => {
        this.backList();
      }
    })
  }
}
