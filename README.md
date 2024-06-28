# bingdict

## 介绍
为了将生词本掌握在本地而写的项目，利用[必应词典](https://cn.bing.com/dict/)翻译英语单词和词组，输出单词的音标和语义，保存到postgreSQL数据库中，并记录该单词翻译的次数。

```shell
usr@PC:~$ bingdict reference
美[ˈref(ə)rəns]，英['ref(ə)rəns]，
n. 参考；编号；提到；查询； 
v. 参考；查阅；给（书等）附参考资料； 
网络释义： 引用；参考文献；参考书； 
```

可结合powershell脚本监听剪切板变化从而自动翻译复制的单词（适用于可以安装powershell的平台），以下是Debian12上的例子，powershell的Get-Clipboard依赖于xclip，有的发行版可能没有，需要自行安装。

```shell
usr@PC:~$ pwsh
PowerShell 7.4.3

CommandType     Name                                               Version    Source
-----------     ----                                               -------    ------
Alias           cwpd -> ClipboardWatcherPurePowershellDocker.ps1              

PS /home/usr> cwpd
美[ɔˈθɔrɪˌteɪtɪv]，英[ɔːˈθɒrɪtətɪv]，
adj. 命令式的；专断的；权威式的；权威性的； 
网络释义： 权威的；有权威的；官方的；

```



## 注意事项

1. [必应词典](https://cn.bing.com/dict/)会检测是否使用了代理（大概），使用代理的人可能需要放行*<u>bing.com</u>*域名
2. 这个项目需要你手动修改代码以满足自己的需要然后再编译，还需要自己拥有个数据库（本地sqlite数据库也行，需要改一下Model.cs和EF core相关的包），不提供也没办法提供完整的release

## 贡献指南

欢迎任何形式的贡献

## 许可证

MIT

