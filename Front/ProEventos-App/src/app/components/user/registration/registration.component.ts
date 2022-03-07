import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from 'src/app/helpers/ValidatorField';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  public form!: FormGroup;

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.validation();
  }

  public get f(): any { return this.form.controls; }

  private validation(): void {

    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('senha', 'confirmeSenha')
    };

    this.form = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      userName: ['', Validators.required],
      senha: ['', [Validators.required, Validators.minLength(8)]],
      confirmeSenha: ['', Validators.required]
    }, formOptions);
  }

}
