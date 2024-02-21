import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChildren, inject } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ValidationModel } from '../../shared/validators/validation.model';
import { LoginModel } from '../models/login.model';
import { AuthService } from '../auth.service';
import { ToasterService } from '../../../core/services/toaster.service';
import { GenericValidator } from '../../shared/validators/forms-error-validator';
import { Observable, fromEvent, merge } from 'rxjs';
import { appConstant } from '../../../core/extenstion/app-constants';
import { ErrorResponse } from '../../../core/models/error-response.model';
import { ResponseResult } from '../../../core/models/response-result.model';

@Component({
  selector: 'netbox-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})

export class LoginComponent implements OnInit, OnDestroy, AfterViewInit {

  public isCollapsed = true;
  isFormSubmitted = false;
  isRequested = false;

  loginModel = new LoginModel();
  userPermissionNames = new Array<string>();
  validationModel: ValidationModel = new ValidationModel();

  loginForm: FormGroup;

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  authService = inject(AuthService);
  formBuilder = inject(FormBuilder);
  router = inject(Router) as Router;
  toasterService = inject(ToasterService);

  constructor() {
    this.validationModel.validationMessages = {
      email: {
        required: 'Email is required',
      },
      password: {
        required: 'Password is required',
      },
    };
    this.validationModel.formsErrorValidator = new GenericValidator(this.validationModel.validationMessages);
  }

  ngOnInit() {
    this.createLoginForm();

    var html = document.getElementsByTagName("html")[0];
    html.classList.add("auth-layout");
    var body = document.getElementsByTagName("body")[0];
    body.classList.add("bg-default");
    this.router.events.subscribe((event) => {
      this.isCollapsed = true;
    });
  }

  ngOnDestroy() {
    var html = document.getElementsByTagName("html")[0];
    html.classList.remove("auth-layout");
    var body = document.getElementsByTagName("body")[0];
    body.classList.remove("bg-default");
  }

  ngAfterViewInit(): void {
    const controlBlurs: Observable<any>[] = this.formInputElements.map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));
    merge(this.loginForm.valueChanges, ...controlBlurs).subscribe(() => {
      this.validate();
    });
  }

  validate(): void {
    this.validationModel.displayMessage = this.validationModel.formsErrorValidator.processMessages(this.loginForm, this.isFormSubmitted);
  }

  createLoginForm() {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  authenticate() {
    this.isFormSubmitted = true;
    this.validate();
    if (this.loginForm.invalid) { return; }

    this.isRequested = true;
    this.loginModel = Object.assign({}, this.loginModel, this.loginForm.value);
    this.authService.login(this.loginModel)
      .subscribe({
        next: (res: ResponseResult<string>) => {
          this.isFormSubmitted = false;
          localStorage.setItem(appConstant.jwtTokenName, JSON.stringify(res.data));
          this.setUserPermissions(JSON.stringify(res.data));
        },
        error: (err: ErrorResponse) => {
          this.isFormSubmitted = this.isRequested = false;
          this.toasterService.error(err);
        }
      });
  }

  setUserPermissions(res: any) {
    let result = JSON.parse(res);
    result.claims.forEach((claim: any) => {
      this.userPermissionNames.push(claim.claimValue);
    });
    localStorage.setItem('claims', JSON.stringify(this.userPermissionNames));
    this.router.navigate(['/admin']);
  }
} 
