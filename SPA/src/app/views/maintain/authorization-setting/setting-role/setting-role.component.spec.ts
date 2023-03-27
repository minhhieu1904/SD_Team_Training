import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SettingRoleComponent } from './setting-role.component';

describe('SettingRoleComponent', () => {
  let component: SettingRoleComponent;
  let fixture: ComponentFixture<SettingRoleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SettingRoleComponent]
    });
    fixture = TestBed.createComponent(SettingRoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
