import { AfterViewInit, Component, ElementRef, OnInit, ViewChild, ViewChildren, inject } from '@angular/core';
import { FormGroup, FormControlName, FormBuilder, Validators } from '@angular/forms';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Observable, fromEvent, merge } from 'rxjs';
import { ErrorResponse } from '../../../core/models/error-response.model';
import { ResponseResult } from '../../../core/models/response-result.model';
import { SearchRequestModel } from '../../../core/models/search-request.model';
import { ModalService } from '../../../core/services/modal.service';
import { ToasterService } from '../../../core/services/toaster.service';
import { ConfirmationModalComponent } from '../../shared/components/confirmation-modal/confirmation-modal.component';
import { GenericValidator } from '../../shared/validators/forms-error-validator';
import { ValidationModel } from '../../shared/validators/validation.model';
import { ServiceTypeModel } from '../models/service-type.model';
import { ServiceTypeService } from '../services/service-type.service';
import { ClientTypeService } from '../services/client-type.service';
import { ClientTypeModel } from '../models/client-type.model';
import { AppPermissions } from '../../../core/extenstion/permissions';

@Component({
  selector: 'netbox-client-type',
  templateUrl: './client-type.component.html',
  styleUrl: './client-type.component.scss'
})
export class ClientTypeComponent implements OnInit, AfterViewInit {

  isBlocked = false;
  pageSizeOptions: number[] = [10, 25, 50, 100];
  searchModel = new SearchRequestModel(10, 1);
  pageIndex = 0;
  displayClientTypeCreation = false;
  isFormSubmitted = false;
  isEdit = false;

  clientTypeForm: FormGroup;
  clientTypeModel: ClientTypeModel;

  clientTypes: ClientTypeModel[] = [];
  clientTypeService = inject(ClientTypeService);
  validationModel: ValidationModel = new ValidationModel();

  p = AppPermissions;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  constructor(private formBuilder: FormBuilder, private toasterService: ToasterService,
    private modalService: ModalService) {
    this.validationModel.validationMessages = {
      clientType: {
        required: 'Client Type is required'
      }
    };
    this.validationModel.formsErrorValidator = new GenericValidator(this.validationModel.validationMessages);
  }

  ngOnInit(): void {
    this.getClientTypes();
    this.createForm();
  }

  getClientTypes() {
    this.isBlocked = true;
    this.clientTypeService.getAll(this.searchModel).subscribe({
      next: (res: ResponseResult<ClientTypeModel[]>) => {
        this.clientTypes = res.data;
        this.searchModel.totalRecords = res.totalRecordCount;
        this.isBlocked = false;
      },
    })
  }

  public pageChanged(event: PageEvent): void {
    this.searchModel.pageSize = event.pageSize
    this.searchModel.pageNumber = event.pageIndex + 1;
    this.getClientTypes();
  }

  ngAfterViewInit(): void {
    this.paginator.pageIndex = this.pageIndex;
    const controlBlurs: Observable<any>[] = this.formInputElements.map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));
    merge(this.clientTypeForm.valueChanges, ...controlBlurs).subscribe(value => {
      this.validationModel.displayMessage = this.validationModel.formsErrorValidator.processMessages(this.clientTypeForm, this.isFormSubmitted);
    });
  }

  displayClientTypeManage() {
    this.isEdit = false;
    this.clientTypeForm.reset();
    this.displayClientTypeCreation = true;
  }


  validate(): void {
    this.validationModel.displayMessage = this.validationModel.formsErrorValidator.processMessages(this.clientTypeForm, this.isFormSubmitted);
  }

  createForm() {
    this.clientTypeForm = this.formBuilder.group({
      clientType: ['', Validators.required]
    });
  }

  create() {
    this.isFormSubmitted = true;
    this.validate();
    if (this.clientTypeForm.invalid) return;

    this.isBlocked = true;
    this.clientTypeModel = Object.assign({}, this.clientTypeModel, this.clientTypeForm.value);
    this.clientTypeService.create(this.clientTypeModel).subscribe({
      next: (serviceTypeModel: ResponseResult<ClientTypeModel>) => {
        this.isBlocked = false;
        this.isFormSubmitted = false;
        this.clientTypeForm.reset();
        this.getClientTypes();
        this.toasterService.successfullyCreated("Client Type");
      },
      error: (err: ErrorResponse) => {
        this.isBlocked = false;
        this.isFormSubmitted = false;
        this.toasterService.error(err);
      }
    })
  }

  update() {
    this.isFormSubmitted = true;
    this.validate();
    if (this.clientTypeForm.invalid) return;

    this.isBlocked = true;
    this.clientTypeModel = Object.assign({}, this.clientTypeModel, this.clientTypeForm.value);
    this.clientTypeService.update(this.clientTypeModel).subscribe({
      next: (serviceTypeModel: object) => {
        this.isBlocked = false;
        this.isEdit = false;
        this.displayClientTypeCreation = false;
        this.isFormSubmitted = false;
        this.clientTypeForm.reset();
        this.getClientTypes();
        this.toasterService.successfullyUpdated("Client Type");
      },
      error: (err: ErrorResponse) => {
        this.isBlocked = false;
        this.isFormSubmitted = false;
        this.toasterService.error(err);
      }
    })
  }

  getTypeById(clientType: ClientTypeModel) {
    this.isBlocked = true
    this.clientTypeService.getById(clientType.id).subscribe({
      next: (clientTypeModel: ResponseResult<ClientTypeModel>) => {
        this.isEdit = true;
        this.isBlocked = false;
        this.displayClientTypeCreation = true;
        this.clientTypeModel = clientTypeModel.data;
        this.clientTypeForm.patchValue(this.clientTypeModel);
      },
      error: (err: ErrorResponse) => {
        this.isBlocked = false;
        this.isEdit = false;
        this.displayClientTypeCreation = false;
        this.isFormSubmitted = false;
        this.toasterService.error(err);
      }
    })
  }

  deleteServiceType(clientType: ClientTypeModel) {
    let options = {
      title: 'Delete Client Type',
      message: 'Are you sure you want to delete the client type ?',
    };
    this.modalService.displayDialog(ConfirmationModalComponent, options)
    this.modalService.confirmed().subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.isBlocked = true;
        this.clientTypeService.deleteClientType(clientType.id).subscribe({
          next: () => {
            this.isBlocked = false;
            this.getClientTypes();
          },
          error: (err: ErrorResponse) => {
            this.isBlocked = false;
            this.toasterService.error(err);
          }
        })
      }
    });
  }
}
