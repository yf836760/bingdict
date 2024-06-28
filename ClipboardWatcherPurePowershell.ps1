$currentText = ''
$originText = Get-Clipboard -Raw
$wordLenLimit = 30

while($true) {
    # Get-Clipboard 命令在linux下依赖于xclip，需要安装xclip包
    $currentText = Get-Clipboard -Raw
    if ([string]::IsNullOrEmpty($currentText) -or [string]::IsNullOrWhiteSpace($currentText)) {
        Start-Sleep -Milliseconds 100
        continue
    }
    if ($currentText -eq $originText) {
        Start-Sleep -Milliseconds 100
        continue
    }
    $originText = $currentText
    $backupCurrentText = $currentText
    $backupCurrentText = $backupCurrentText.Replace("`r`n", " ")
    $len = $backupCurrentText.Length
    if ($len -ge $wordLenLimit) {
        Start-Sleep -Milliseconds 100
        continue
    }
    #可以将bingdict添加到环境变量，代替绝对路径
    Invoke-Expression "C:\Users\usr\tools\bingdict.exe '$backupCurrentText'; Write-Host ''" 
    
    Start-Sleep -Milliseconds 100
}
