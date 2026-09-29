$(document).ready(function () {

    $.ajax({
        url: '/SidebarMenu/GetSidebarPages',
        method: 'GET',
        success: function (pages) {
            let html = '<ul class="nav flex-column bg-white p-3 rounded shadow-sm">';
                html += '<li class="nav-item mb-3"><h5 class="text-primary fw-bold text-center">Menu</h5><hr/></li>';

            pages.forEach(function (page) {
                html += `<li class="nav-item">
                            <a class="nav-link text-dark" href="/${page.controller}/${page.action}?PageId=${page.pageId}">
                               <i class="bi bi-arrow-right-short me-2"></i> ${page.pageName}
                            </a>
                         </li>`;
            });

            html += '</ul>';
            $('#sidebarMenu').html(html);
        },
        error: function () {
            console.error("Failed to load admin sidebar.");
        }
    });

    const rowCount = parseInt($('#permissions-table').data('count'));

    for (let i = 0; i < rowCount; i++) {
        $(`#add_${i}, #edit_${i}, #editpermission_${i}, #delete_${i}`).on('change', function () {
            const isAdd = $(`#add_${i}`).prop('checked');
            const isEdit = $(`#edit_${i}`).prop('checked');
            const isEditPermission = $(`#editpermission_${i}`).prop('checked');
            const isDelete = $(`#delete_${i}`).prop('checked');

            if (isAdd || isEdit || isDelete || isEditPermission) {
                $(`#view_${i}`).prop('checked', true);
            }
        });

        $(`#view_${i}`).on('change', function () {
            if (!$(this).prop('checked')) {
                $(`#add_${i}`).prop('checked', false);
                $(`#edit_${i}`).prop('checked', false);
                $(`#editpermission_${i}`).prop('checked', false);
                $(`#delete_${i}`).prop('checked', false);
            }
        });
    }
    
});

function Dashboard() {
    window.location.href = '/Dashboard/Dashboard';
}
function UserList() {
    window.location.href = '/UserList/UserList';
}
function CreateUser() {
    $.ajax({
        url: '/UserList/CreateUser',
        type: 'GET',
        success: function () {
            window.location.href = '/UserList/CreateUser';
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });
}
function DeleteUser(userId) {
    $.ajax({
        url: '/UserList/DeleteUser',
        type: 'POST',
        data: { userId: userId },
        success: function () {
            //window.location.href = '/UserList/UserList';
            window.location.reload();
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });
}
function StudentList() {
    $.ajax({
        url: '/StudentList/StudentList',
        type: 'GET',
        success: function () {
            window.location.href = '/StudentList/StudentList';
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });
}
function EmployeeList() {
    $.ajax({
        url: '/EmployeeList/EmployeeList',
        type: 'GET',
        success: function () {
            window.location.href = '/EmployeeList/EmployeeList';
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });
}
function CustomerList() {
    $.ajax({
        url: '/CustomerList/CustomerList',
        type: 'GET',
        success: function () {
            window.location.href = '/CustomerList/CustomerList';
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });
}
function UserRoleList() {
    $.ajax({
        url: '/UserRole/UserRole',
        type: 'GET',
        success: function () {
            window.location.href = '/UserRole/UserRole';
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });
}
function DeleteStudent(studentId) {
    $.ajax({
        url: '/StudentList/Delete',
        type: 'POST',
        data: { id: studentId }, 
        success: function () {
            //window.location.href = '/StudentList/StudentList';
            window.location.reload();
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });
}
function DeleteEmployee(employeeId) {
    $.ajax({
        url: '/EmployeeList/Delete',
        type: 'POST',
        data: { id: employeeId },
        success: function () {
            //window.location.href = '/EmployeeList/EmployeeList';
            window.location.reload();
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });
}
function DeleteCustomer(customerId) {
    $.ajax({
        url: '/CustomerList/Delete',
        type: 'POST',
        data: { id: customerId },
        success: function () {
            //window.location.href = '/CustomerList/CustomerList';
            window.location.reload();
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });
}
function DeleteUserRole(roleId) {
    $.ajax({
        url: '/UserRole/Delete',
        type: 'POST',
        data: { id: roleId }, 
        success: function () {
            //window.location.href = '/UserRole/UserRole';
            window.location.reload();
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });
}
function Logout() {
    $.ajax({
        url: '/Account/Logout',
        type: 'GET',
        success: function () {
            window.location.href = '/Account/Login';
            window.location.reload();
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });
}
