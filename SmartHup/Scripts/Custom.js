function request(id,request) {
swal({
  title: 'Are you sure?',
  text: "",
  //type: 'warning',
  showCancelButton: true,
  confirmButtonColor: '#3085d6',
  cancelButtonColor: '#d33',
  confirmButtonText: 'Yes'
}).then((result) => {
  if (result.value) {
    //swal(
    //  'Deleted!',
    //  'Your file has been deleted.',
    //  'success'
      //)
      window.location.href = '/Contracts/Requests/' + id + '?status=' + request;

  }
})
}