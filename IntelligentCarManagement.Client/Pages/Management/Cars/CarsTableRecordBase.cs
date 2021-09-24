using IntelligentCarManagement.Client.Services;
using IntelligentCarManagement.Client.Shared;
using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Pages.Management.Cars
{
    public class CarsTableRecordBase : ComponentBase
    {
        [Parameter] public Car Car { get; set; }

        [Parameter] public EventCallback OnDataChange { get; set; }

        [Inject] public IDialogService DialogService { get; set; }

        [Inject] public ICarService CarService { get; set; }

        [Inject] public ISnackbar Snackbar { get; set; }

        protected string message;
        protected bool isSuccess = false;
        protected bool isFail = false;

        protected async Task RemoveCar()
        {
            var state =  await CarService.RemoveCarAsync(Car.Id);

            if (state is true)
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
                Snackbar.Add("Car deleted successfully.", Severity.Success);
            }
            else
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
                Snackbar.Add("Something went wrong. Couldn't remove the car.", Severity.Error);
            }
            await OnDataChange.InvokeAsync(Car.Id);
        }

        protected async Task OpenDeleteDialogAsync()
        {
            var parameters = new DialogParameters
            {
                { "Id", Car.Id },
                { "ContentText", $"Are you sure you want to delete the record with the id {Car.Id} ?" },
                { "ButtonText", "Delete" },
                { "Color", Color.Error }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<DialogDeleteWindow>("Delete", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await RemoveCar();
            }
        }
        protected async Task OpenEditDialogAsync()
        {
            var parameters = new DialogParameters
            {
                { "Car", Car }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            var dialog = DialogService.Show<DialogEditCarWindow>("Edit", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await OnDataChange.InvokeAsync(Car.Id);
            }
        }
    }
}
