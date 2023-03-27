import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MSPackage } from '@models/mS_Package';
import { MSPackageService } from '@services/ms-package.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {
  msPackage: MSPackage = <MSPackage>{
    manuf: '',
    packageNo: '',
    packageQty: 0
  };

  constructor(private service: MSPackageService,
    private router: Router 
    ) { }

  ngOnInit(): void {
  }

  backList() {
    this.router.navigate(['maintain/package-data-maintain']);
  }

  save() {
    this.service.addNew(this.msPackage).subscribe({
      next: res => {
        if(res.isSuccess)
        alert('Them thanh cong')
      }, error: (err) => alert('Them that bai')
    })
  }

  saveAndNext() {
    this.service.addNew(this.msPackage).subscribe({
      next: res => {
        if(res.isSuccess)
        alert('Them thanh cong')
      }, error: (err) => alert('Them that bai'),
      complete: () => {
        this.backList();
      }
    })
  }
}
