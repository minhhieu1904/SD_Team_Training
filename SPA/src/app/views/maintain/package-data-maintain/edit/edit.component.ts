import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MSPackage } from '@models/mS_Package';
import { MSPackageService } from '@services/ms-package.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {
  msPackage: MSPackage = <MSPackage>{
    manuf: '',
    packageNo: '',
    packageQty: 0
  }
  constructor(
    private service: MSPackageService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    let manuf: '';
    let packageNo: '';
    this.route.params.subscribe(params => {
      manuf = params['manuf'];
      packageNo = params['packageNo'];
      this.getItem(manuf, packageNo);
    });
      this.updateItem();
  }

  backList() {
    this.router.navigate(['maintain/package-data-maintain']);
  }

  getItem(manuf: string, packageNo: string) {
    this.service.getItem(manuf, packageNo).subscribe({
      next: res => {
        this.msPackage = res;
      }, error(err) {
        console.log(err);
                
      },complete: () => {
        console.log('complete');        
      }
    })
  }

  updateItem() {
    this.service.updateItem(this.msPackage).subscribe({
      next: res => {
        if(res.isSuccess)
          alert('Update thanh cong');
      }, error: (err) => 
      {
        alert('Update that bai')
      },complete: () => {
        console.log('complete');        
      }
    })
  }

  updateItemAndNext() {
    this.service.updateItem(this.msPackage).subscribe({
      next: res => {
        if(res.isSuccess)
          alert('Update thanh cong');
      }, error: (err) => 
      {
        alert('Update that bai')
      },complete: () => {
        this.backList();   
      }
    })
  }
}
