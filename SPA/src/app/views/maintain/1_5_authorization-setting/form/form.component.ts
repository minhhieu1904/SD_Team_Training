import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription, finalize } from 'rxjs';
import { AuthorizationSettingService } from '@services/maintain/authorization-setting.service';
import { Users } from '@models/common/users';
import { InjectBase } from '@utilities/inject-base-app';
import { IconButton } from '@constants/common.constants';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent extends InjectBase implements OnInit, OnDestroy {
  //Khai báo biến iconButton để xử dụng icon trong template
  iconButton = IconButton;
  //Khai báo FormGroup để quản lý form và biến user để lưu dữ liệu người dùng
  userForm: FormGroup;
  user: Users = <Users>{ is_active: true }

  //Biến để lưu đối tượng Subscription để theo dõi sự kiện ngOnDestroy
  private layoutSubscription: Subscription;

  constructor(
    private formBuilder: FormBuilder,
    private service: AuthorizationSettingService,
  ) {
    super();
  }


  ngOnInit(): void {
    //Khởi tạo form và theo dõi sự thay đổi dữ liệu hiện tại
    this.initForm()
    this.subscribeToLayoutChanges()
  }

  ngOnDestroy(): void {
    //Hủy theo dõi khi component bị hủy
    this.layoutSubscription?.unsubscribe();
  }

  //Khởi tạo form với các trường và kiểm tra ban đầu
  private initForm(): void {
    this.userForm = this.formBuilder.group({
      type: ['edit'],
      account: ['', Validators.required],
      password: ['', Validators.required],
      name: ['', Validators.required],
      email: ['', [Validators.email]],
      is_active: [true],
      update_by: [''],
      update_time: [new Date()],
    })
  }

  getPasswordStrength(password: string): string {
    if (password.length < 8) {
      return 'Weak';
    }
    if (password.length >= 12) {
      if (/[a-zA-Z]/.test(password) && /[0-9]/.test(password) && /[^a-zA-Z0-9]/.test(password)) {
        return 'Strong';
      }
    }
    return 'Medium';
  }

  getPasswordStrengthClass(password: string): string {
    const strength = this.getPasswordStrength(password);
    return `password-strength-${strength.toLowerCase()}`;
  }

  getPasswordStrengthMessage(password: string): string {
    const strength = this.getPasswordStrength(password);
    return `Password Strength: ${strength}`;
  }

  isPasswordNotEmpty(): boolean {
    return !!this.userForm.get('password')?.value;
  }

  //Theo dõi thay đổi trong dữ liệu hiện tại và cập nhật form tương ứng
  private subscribeToLayoutChanges(): void {
    this.layoutSubscription = this.service.layoutSource.subscribe(receiveData => {
      if (receiveData) {
        this.user = { ...receiveData };
        this.userForm.patchValue(receiveData);
      } else {
        this.back();
      }
    });
  }

  //Xử lý khi người dùng lưu thay đổi
  saveChange(type?: string) {
    this.spinnerService.show();
    const formValue = this.userForm.value;
    const messageType = formValue.type === 'add' ? 'Create' : 'Update';
    const request$ = formValue.type === 'add'
      ? this.service.addNew(formValue)
      : this.service.edit(formValue);

    //Gửi yêu cầu và xử lý kết quả
    request$.pipe(
      //Sử dụng 'finalize' để đảm bảo rằng việc ẩn spinner luôn được thực hiện
      finalize(() => this.spinnerService.hide())
    ).subscribe({
      next: (res) => {
        const isSuccess = res.isSuccess;
        const successMsg = isSuccess
          ? `System.Message.${messageType}OKMsg`
          : `System.Message.${messageType}ErrorMsg`;
        const caption = isSuccess ? 'Success' : 'Error';
        const method = isSuccess ? 'success' : 'error';

        //Hiển thị thông báo dựa trên kết quả
        this.snotifyService[method](
          this.translateService.instant(successMsg),
          this.translateService.instant(`System.Caption.${caption}`)
        );

        //Nếu thành công, quay trở lại trang chính và reset form
        if (isSuccess) {
          this.back();
          if (type === 'next') {
            this.userForm.reset();
          }
        }
      },
      error: () => {
        const errorMsg = `System.Message.${messageType}ErrorMsg`
        this.snotifyService.error(
          this.translateService.instant(errorMsg),
          this.translateService.instant('System.Caption.Error'));
      }
    })
  }

  //Quay lại trang chính
  back() {
    this.router.navigate(['maintain/authorization-setting'])
  }
}
