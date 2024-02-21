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
import { JobTypeModel } from '../models/job-type.model';
import { JobTypeService } from '../services/job-type.service';
import { AppPermissions } from '../../../core/extenstion/permissions';

@Component({
  selector: 'netbox-job-types',
  templateUrl: './job-types.component.html',
  styleUrl: './job-types.component.scss'
})
export class JobTypesComponent implements OnInit, AfterViewInit {

  isBlocked = false;
  pageSizeOptions: number[] = [10, 25, 50, 100];
  searchModel = new SearchRequestModel(10, 1);
  pageIndex = 0;
  displayJobTypeCreation = false;
  isFormSubmitted = false;
  isEdit = false;

  jobTypeForm: FormGroup;
  jobTypeModel: JobTypeModel;

  jobTypes: JobTypeModel[] = [];
  jobTypeService = inject(JobTypeService);
  validationModel: ValidationModel = new ValidationModel();

  p = AppPermissions;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  constructor(private formBuilder: FormBuilder, private toasterService: ToasterService,
    private modalService: ModalService) {
    this.validationModel.validationMessages = {
      name: {
        required: 'Job Type is required'
      }
    };
    this.validationModel.formsErrorValidator = new GenericValidator(this.validationModel.validationMessages);
  }

  ngOnInit(): void {
    this.getJobTypes();
    this.createForm();
  }

  getJobTypes() {
    this.isBlocked = true;
    this.jobTypeService.getAll(this.searchModel).subscribe({
      next: (res: ResponseResult<JobTypeModel[]>) => {
        this.jobTypes = res.data;
        this.searchModel.totalRecords = res.totalRecordCount;
        this.isBlocked = false;
      },
    })
  }

  public pageChanged(event: PageEvent): void {
    this.searchModel.pageSize = event.pageSize
    this.searchModel.pageNumber = event.pageIndex + 1;
    this.getJobTypes();
  }

  ngAfterViewInit(): void {
    this.paginator.pageIndex = this.pageIndex;
    const controlBlurs: Observable<any>[] = this.formInputElements.map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));
    merge(this.jobTypeForm.valueChanges, ...controlBlurs).subscribe(value => {
      this.validationModel.displayMessage = this.validationModel.formsErrorValidator.processMessages(this.jobTypeForm, this.isFormSubmitted);
    });
  }

  displayJobTypeManage() {
    this.isEdit = false;
    this.jobTypeForm.reset();
    this.displayJobTypeCreation = true;
  }


  validate(): void {
    this.validationModel.displayMessage = this.validationModel.formsErrorValidator.processMessages(this.jobTypeForm, this.isFormSubmitted);
  }

  createForm() {
    this.jobTypeForm = this.formBuilder.group({
      name: ['', Validators.required]
    });
  }

  create() {
    this.isFormSubmitted = true;
    this.validate();
    if (this.jobTypeForm.invalid) return;

    this.isBlocked = true;
    this.jobTypeModel = Object.assign({}, this.jobTypeModel, this.jobTypeForm.value);
    this.jobTypeService.create(this.jobTypeModel).subscribe({
      next: (jobTypeModel: ResponseResult<JobTypeModel>) => {
        this.isBlocked = false;
        this.isFormSubmitted = false;
        this.jobTypeForm.reset();
        this.getJobTypes();
        this.toasterService.successfullyCreated("Job Type");
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
    if (this.jobTypeForm.invalid) return;

    this.isBlocked = true;
    this.jobTypeModel = Object.assign({}, this.jobTypeModel, this.jobTypeForm.value);
    this.jobTypeService.update(this.jobTypeModel).subscribe({
      next: (jobTypeModel: object) => {
        this.isBlocked = false;
        this.isEdit = false;
        this.displayJobTypeCreation = false;
        this.isFormSubmitted = false;
        this.jobTypeForm.reset();
        this.getJobTypes();
        this.toasterService.successfullyUpdated("Job Type");
      },
      error: (err: ErrorResponse) => {
        this.isBlocked = false;
        this.isFormSubmitted = false;
        this.toasterService.error(err);
      }
    })
  }

  getTypeById(jobTypeModel: JobTypeModel) {
    this.isBlocked = true
    this.jobTypeService.getById(jobTypeModel.id).subscribe({
      next: (jobTypeModel: ResponseResult<JobTypeModel>) => {
        this.isEdit = true;
        this.isBlocked = false;
        this.displayJobTypeCreation = true;
        this.jobTypeModel = jobTypeModel.data;
        this.jobTypeForm.patchValue(this.jobTypeModel);
      },
      error: (err: ErrorResponse) => {
        this.isBlocked = false;
        this.isEdit = false;
        this.displayJobTypeCreation = false;
        this.isFormSubmitted = false;
        this.toasterService.error(err);
      }
    })
  }

  deleteServiceType(jobType: JobTypeModel) {
    let options = {
      title: 'Delete Job Type',
      message: 'Are you sure you want to delete the job type ?',
    };
    this.modalService.displayDialog(ConfirmationModalComponent, options)
    this.modalService.confirmed().subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.isBlocked = true;
        this.jobTypeService.deleteJobType(jobType.id).subscribe({
          next: () => {
            this.isBlocked = false;
            this.getJobTypes();
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
