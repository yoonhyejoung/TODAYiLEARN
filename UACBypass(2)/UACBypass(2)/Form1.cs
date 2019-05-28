using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UACBypass_2_
{
    //작업 스케줄러를 이용해서 UAC 우회 방법
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Insert();

        }

        private void Insert()
        {
            using (TaskService taskService = new TaskService())
            {
                TaskDefinition taskDefinition = taskService.NewTask();

                #region 일반
                // taskDefinition.RegistrationInfo.Description = "";
                //사용자가 로그인 할 경우 실행
                //taskDefinition.Principal.UserId = string.Concat(Environment.UserDomainName, "\\", Environment.UserName);
                //taskDefinition.Principal.LogonType = TaskLogonType.S4U;
                //가장 높은 수준의 권한으로 실행
                taskDefinition.Principal.RunLevel = TaskRunLevel.Highest;
                #endregion

                #region 동작
               
                taskDefinition.Actions.Add(new ExecAction(@"C:\Users\Administrator\Desktop\YClean\YClean\bin\Release\YClean.exe"));

                #endregion

                #region 조건탭

               // 유휴상태
                // 컴퓨터가 다음 시간 동안 유휴 상태인 경우에만 작업시작
                taskDefinition.Settings.RunOnlyIfIdle = false;
                //taskDefinition.Settings.IdleSettings.IdleDuration = TimeSpan.FromMinutes(10);
                // 다음시간 동안 유휴 상태 대기
                //taskDefinition.Settings.IdleSettings.WaitTimeout = TimeSpan.FromHours(1);
                // 컴퓨터의 유휴 상태가 끝나면 중지
                // taskDefinition.Settings.IdleSettings.StopOnIdleEnd = false;   // 기본값 true
                                                                              // 유휴 상태가 재개되면 다시 시작
                //taskDefinition.Settings.IdleSettings.RestartOnIdle = false;   // 기본값 false


                // 전원
                // 컴퓨터의 AC 전원이 켜져 있는 경우에만 작업 시작
                taskDefinition.Settings.DisallowStartIfOnBatteries = false;
               
                // 네트워크
                // 다음 네트워크 연결을 사용할 수 있는 경우에만 시작
                taskDefinition.Settings.RunOnlyIfNetworkAvailable = false;

                #endregion


                #region 설정탭
                // 요청시 작업이 실행되도록 허용
                taskDefinition.Settings.AllowDemandStart = true;

                //예약된 시간을 놓친 경우 가능한 대로 빨리 작업 시작
                taskDefinition.Settings.StartWhenAvailable = true;

                //요청할 때 실행 중인 작업이 끝나지 않으면 강제로 작업 중지
                taskDefinition.Settings.AllowHardTerminate = true;
                
                //다음 시간 이상 작업이 실행되면 중지
                taskDefinition.Settings.ExecutionTimeLimit = new TimeSpan(0);

             
                //
                #endregion


               

                // 등록
                taskService.RootFolder.RegisterTaskDefinition("YClean", taskDefinition);
            }
        }






}
}
