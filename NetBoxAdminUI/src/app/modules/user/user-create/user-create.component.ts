import { KeyValue } from '@angular/common';
import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges, ViewChild, ViewChildren } from '@angular/core';
import { FormGroup, FormControlName, FormBuilder, Validators } from '@angular/forms';
import { Observable, fromEvent, merge, startWith, map } from 'rxjs';
import { ErrorResponse } from '../../../core/models/error-response.model';
import { ResponseResult } from '../../../core/models/response-result.model';
import { SearchRequestModel } from '../../../core/models/search-request.model';
import { TimeZoneModel } from '../../../core/models/time-zone.model';
import { ToasterService } from '../../../core/services/toaster.service';
import { GenericValidator } from '../../shared/validators/forms-error-validator';
import { ValidationModel } from '../../shared/validators/validation.model';
import { UserRoleModel, UserClaimModel } from '../models/user-role.model';
import { UserModel } from '../models/user.model';
import { UserRoleService } from '../services/user-role.service';
import { UserService } from '../services/user.service';
import { patterns } from '../../../core/extenstion/regex-patterns';
import { UserPermissionViewComponent } from '../user-permission-view/user-permission-view.component';

@Component({
  selector: 'netbox-user-create',
  templateUrl: './user-create.component.html',
  styleUrl: './user-create.component.scss',
})
export class UserCreateComponent implements OnInit, AfterViewInit {
  isBlocked = false;
  isFormSubmitted = false;
  displayStyle = 'none';
  displayPermissionStyle = 'none';
  isView = false;
  roleId: string = '';
  currentRoleId: string;
  roleName: string = '';
  pageSize = 1;
  isPermissionSelection = false;
  isPermissionEdit = false;
  isPasswordHide = true;
  isConfirmPasswordHide = true;
  isAllSelected = false;

  userForm: FormGroup;
  timeZones: TimeZoneModel[];
  userRoles: UserRoleModel[];
  userModel: UserModel;
  searchRequestModel = new SearchRequestModel(100000, 1);
  selectedPermissions = new Array<string>();
  retrievedPermissions = new Array<UserClaimModel>();
  permissionNames = new Array<string>();
  timeZoneFilteredOption: Observable<TimeZoneModel[]> | undefined;
  roleFilteredOption: Observable<UserRoleModel[]> | undefined;

  validationModel: ValidationModel = new ValidationModel();

  @Input() isDisplay: boolean;
  @Input() userId: string;
  @Input() isEdit: boolean = false;
  @Output() userRolesList = new EventEmitter<boolean>();
  @Output() userCreationClosed = new EventEmitter<boolean>();

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  @ViewChild('userPermissionComponent') userPermissionComponent: UserPermissionViewComponent;

  constructor(
    private formBuilder: FormBuilder,
    private toasterService: ToasterService,
    private userRoleService: UserRoleService,
    private userService: UserService) {
    this.validationModel.validationMessages = {
      userName: {
        required: 'Username is required'
      },
      email: {
        required: 'Email is required',
        pattern: 'Please enter valid email format'
      },
      firstName: {
        required: 'First name is required',
      },
      lastName: {
        required: 'Last name is required'
      },
      password: {
        required: 'Password is required'
      },
      confirmPassword: {
        required: 'Confirm password is required'
      },
      role: {
        required: 'User role is required'
      },
      displayName: {
        required: 'Display name is required'
      }
    };
    this.validationModel.formsErrorValidator = new GenericValidator(this.validationModel.validationMessages);
  }

  ngOnInit(): void {
    this.createUserForm();
    this.getUserRoles();
  }

  show() {
    this.isEdit = false;
    this.isDisplay = true;
    this.openPopup();
  }

  edit(userId: string) {
    this.userId = userId;
    this.isEdit = true;
    this.editUser(this.userId);
  }

  ngAfterViewInit(): void {
    const controlBlurs: Observable<any>[] = this.formInputElements.map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));
    merge(this.userForm.valueChanges, ...controlBlurs).subscribe(value => {
      this.validationModel.displayMessage = this.validationModel.formsErrorValidator.processMessages(this.userForm, this.isFormSubmitted);
    });
  }

  validate(): void {
    this.validationModel.displayMessage = this.validationModel.formsErrorValidator.processMessages(this.userForm, this.isFormSubmitted);
  }

  createUserForm() {
    this.userForm = this.formBuilder.group({
      userName: ['', Validators.required],
      email: ['', [Validators.required, Validators.pattern(patterns.email)]],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      role: ['', Validators.required],
      timeZone: ['Sri Lanka Standard Time'],
      displayName: ['', Validators.required],
      nicNumber: [''],
      mobileNumber: ['']
    });
  }

  getUserRoles() {
    this.isBlocked = true;
    this.userRoleService.getAll(this.searchRequestModel).subscribe({
      next: (roles: ResponseResult<UserRoleModel[]>) => {
        this.userRoles = roles.data;
        this.autoCompleteRole();
        this.isBlocked = false;
      },
      error: (err: ErrorResponse) => {
        this.isBlocked = false;
        this.toasterService.error(err);
      }
    });
  }

  autoCompleteRole() {
    this.roleFilteredOption = this.userForm.get('role')!.valueChanges.pipe(
      startWith(''),
      map(value => this._roleFilter(value))
    )
  }

  private _roleFilter(value: string): UserRoleModel[] {
    if (value) {
      const filterValue = value.toLowerCase();
      return this.userRoles.filter(option => option.roleName.toLowerCase().includes(filterValue));
    }
    return this.userRoles;
  }

  onPermissionProcess(event: any, retrievedRoleName?: string) {
    this.selectedPermissions = [];
    this.isPermissionEdit = false;
    this.roleName = event == '' ? retrievedRoleName : event.option.value;
    this.roleId = this.userRoles.find(f => f.roleName == this.roleName)?.roleId as string;
    this.currentRoleId = this.roleId;
    if (!this.isEdit)
      this.userPermissionComponent.loadPermissions(this.roleId, this.isPermissionEdit, this.isPermissionSelection, this.retrievedPermissions, this.selectedPermissions);
  }

  getPermissionTemplateById() {
    this.isPermissionSelection = true;
    this.isView = true;
    this.selectedPermissions = this.selectedPermissions;
    this.displayPermissionStyle = "block";
  }

  selectedPermissionNames($event: any) {
    this.roleId = "";
    this.retrievedPermissions = [];
    this.selectedPermissions = [];
    this.permissionNames = [];
    this.permissionNames.push(...$event);
    this.selectedPermissions.push(...this.permissionNames);
  }

  addUserPermission() {
    this.roleId = '';
    this.selectedPermissions = [...this.permissionNames];
    this.displayPermissionStyle = "none";
  }

  resetPermission() {
    if (this.isPermissionEdit) {
      this.retrievedPermissions = [...this.userModel.claims];
    }
    this.roleId = this.currentRoleId;
    this.isPermissionSelection = false;

    this.userPermissionComponent.loadPermissions(this.roleId, this.isPermissionEdit, this.isPermissionSelection, this.retrievedPermissions, this.selectedPermissions);
  }

  resetClicked() {
    this.userForm.reset();
    this.isAllSelected = false;
    this.roleId = '';
    this.resetPermission();
  }

  openPopup() {
    this.isFormSubmitted = false;
    this.validate();
    this.autoCompleteRole();
    this.displayStyle = "block";
  }

  closePopup() {
    this.userId = "";
    this.isAllSelected = false;
    this.isEdit = false;
    if (this.userModel != undefined) {
      this.userModel.firstName = "";
      this.userModel.lastName = "";
    }
    this.createUserForm();
    this.userCreationClosed.emit(false);
    this.displayStyle = "none";
  }

  closePermissionPopup() {
    this.roleId = '';
    this.isPermissionSelection = false;
    this.displayPermissionStyle = "none";
  }

  createUser() {
    this.isFormSubmitted = true;
    this.validate();
    if (this.userForm.invalid) { return; }
    this.isBlocked = true;
    this.userModel = Object.assign({}, this.userModel, this.userForm.value);

    const isMatch = this.userModel.password == this.userModel.confirmPassword;
    if (!isMatch) {
      this.isBlocked = false;
      return this.toasterService.warning("Confirm password and password does not match");
    }

    this.userModel.permissions = this.selectedPermissions;
    this.userService.create(this.userModel)
      .subscribe({
        next: () => {
          this.isFormSubmitted = false;
          this.isBlocked = false;
          this.createUserForm();
          this.userModel.firstName = "";
          this.userModel.lastName = "";
          this.autoCompleteRole();
          this.closePopup();
          this.isDisplay = false;
          this.userRolesList.emit(true);
          this.toasterService.successfullyCreated("User");
        },
        error: (err: ErrorResponse) => {
          this.isBlocked = false;
          this.isFormSubmitted = false;
          this.toasterService.error(err);
        }
      });
  }

  editUser(id: string) {
    this.isBlocked = true;
    this.userService.getById(id)
      .subscribe({
        next: (user: ResponseResult<UserModel>) => {
          this.patchUser(user.data);
          this.autoCompleteRole();
          this.isBlocked = false;
        },
        error: (err: ErrorResponse) => {
          this.isBlocked = false;
          this.isEdit = false;
          this.toasterService.error(err);
        }
      });
  }

  patchUser(userModel: UserModel) {
    this.userModel = userModel;
    this.userForm.patchValue({
      id: userModel.id,
      userName: userModel.userName,
      email: userModel.email,
      password: "test",
      confirmPassword: "test",
      firstName: userModel.firstName,
      lastName: userModel.lastName,
      role: userModel.roles[0],
      claims: userModel.claims,
      displayName: userModel.displayName,
      nicNumber: userModel.nicNumber,
      mobileNumber: userModel.mobileNumber
    });

    this.retrievedPermissions = [...userModel.claims];
    this.onPermissionProcess('', this.userModel.roles[0]);
    this.isPermissionEdit = true;
    this.userPermissionComponent.loadPermissions(this.roleId, this.isPermissionEdit, this.isPermissionSelection, this.retrievedPermissions, this.selectedPermissions);
    this.openPopup();
  }

  updateUser() {
    this.isFormSubmitted = true;
    this.validate();
    if (this.userForm.invalid) { return; }

    let userPermission = new Array<string>();
    userPermission = [];
    this.isBlocked = true;
    this.userModel = Object.assign({}, this.userModel, this.userForm.value);

    if (this.selectedPermissions.length == 0) {
      this.userModel.permissions = [];
    }
    else {
      this.userModel.permissions = this.selectedPermissions
    }

    this.userService.update(this.userModel)
      .subscribe({
        next: (res: any) => {
          this.isFormSubmitted = false;
          this.isBlocked = false;
          this.userId = "";
          this.createUserForm();
          this.userModel.firstName = "";
          this.userModel.lastName = "";
          this.autoCompleteRole();
          this.closePopup();
          this.isDisplay = false;
          this.userRolesList.emit(true);
          this.toasterService.successfullyUpdated("User");
        },
        error: (err: ErrorResponse) => {
          this.isBlocked = false;
          this.isFormSubmitted = false;
          this.toasterService.error(err);
        }
      });
  }

}
