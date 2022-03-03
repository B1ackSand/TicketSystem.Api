<template>
  <div class="app-container">

    <!--    修改用户-->
    <el-dialog :visible.sync="dialogFormVisible">
      <el-form
        ref="dataForm"
        :model="bookerForm"
        label-position="left"
        label-width="90px"
        style="width: 400px; margin-left:50px;"
      >
        <el-form-item label="用户名" prop="userName">
          <el-input v-model="bookerForm.userName" />
        </el-form-item>
        <el-form-item label="密码" prop="bookerPwd">
          <el-input v-model="bookerForm.bookerPwd" />
        </el-form-item>
        <el-form-item label="姓" prop="firstName">
          <el-input v-model="bookerForm.firstName" />
        </el-form-item>
        <el-form-item label="名" prop="lastName">
          <el-input v-model="bookerForm.lastName" />
        </el-form-item>
        <el-form-item label="性别" prop="gender">
          <el-input v-model="bookerForm.gender" />
        </el-form-item>
        <el-form-item label="手机号码" prop="phoneNum">
          <el-input v-model="bookerForm.phoneNum" />
        </el-form-item>
        <el-form-item label="出生日期" prop="dateOfBirth">
          <el-input v-model="bookerForm.dateOfBirth" />
        </el-form-item>

      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">返回</el-button>
        <el-button type="primary" @click="updateData()">修改</el-button>
      </div>
    </el-dialog>

    <!--    添加用户-->
    <el-dialog :visible.sync="dialogAddVisible">
      <el-form
        ref="dataForm"
        :model="bookerForm"
        label-position="left"
        label-width="90px"
        style="width: 400px; margin-left:50px;"
      >
        <el-form-item label="用户名" prop="userName">
          <el-input v-model="bookerForm.userName" />
        </el-form-item>
        <el-form-item label="密码" prop="bookerPwd">
          <el-input v-model="bookerForm.bookerPwd" />
        </el-form-item>
        <el-form-item label="姓" prop="firstName">
          <el-input v-model="bookerForm.firstName" />
        </el-form-item>
        <el-form-item label="名" prop="lastName">
          <el-input v-model="bookerForm.lastName" />
        </el-form-item>
        <el-form-item label="性别" prop="gender">
          <el-input v-model="bookerForm.gender" />
        </el-form-item>
        <el-form-item label="手机号码" prop="phoneNum">
          <el-input v-model="bookerForm.phoneNum" />
        </el-form-item>
        <el-form-item label="出生日期" prop="dateOfBirth">
          <el-input v-model="bookerForm.dateOfBirth" />
        </el-form-item>

      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="cancelAddUser;dialogAddVisible = false;">返回</el-button>
        <el-button type="primary" @click="addUser()">添加</el-button>
      </div>
    </el-dialog>

    <!--    界面-->
    <el-table
      :data="tableData"
      stripe
      style="width: 100%"
    >
      <el-table-column
        prop="bookerId"
        label="订票人ID"
        width="300px"
      />

      <el-table-column
        prop="userName"
        label="用户名"
        width="180px"
      />

      <el-table-column
        prop="firstName"
        label="姓"
        width="100px"
      />

      <el-table-column
        prop="lastName"
        label="名"
        width="100px"
      />

      <el-table-column
        prop="bookerPwd"
        label="用户密码"
        width="180px"
      />

      <el-table-column
        prop="gender"
        label="性别"
        width="100px"
        :formatter="formatGender"
      />

      <el-table-column
        prop="phoneNum"
        label="手机号码"
        width="180px"
      />

      <el-table-column
        prop="timeOfRegister"
        label="注册时间"
        width="180px"
      />

      <el-table-column
        prop="dateOfBirth"
        label="出生日期"
        width="100px"
      />

      <el-table-column label="修改操作" align="center" min-width="150" width="200">
        <template slot-scope="scope">
          <el-button type="info" @click="modifyUser(scope.row);dialogFormVisible = true;">修改</el-button>
          <el-button type="info" @click="deleteUser(scope.row.bookerId)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>

    <el-table-column label="页面操作" align="center" min-width="150">
      <el-button type="info" @click="back">上一页</el-button>
      <el-button type="info" @click="next">下一页</el-button>
      <el-button type="info" @click="cancelAddUser();dialogAddVisible = true;">添加</el-button>
    </el-table-column>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  data() {
    return {
      cur: 1,
      size: 10,
      bookerForm: {
        bookerWx: '0',
        userName: '',
        bookerPwd: '',
        firstName: '',
        lastName: '',
        gender: '',
        phoneNum: '',
        dateOfBirth: ''
      },
      dialogFormVisible: false,
      dialogAddVisible: false,
      tableData: [],
      addData: []
    }
  },
  mounted: function() {
    this.show()
  },
  methods: {
    back() {
      const that = this
      if (this.cur === 1) {
        this.$message('已是第一页')
      } else {
        this.cur--
        axios.get('https://localhost:7162/api/bookers/getallbookers', {
          params: {
            PageNumber: this.cur,
            PageSize: this.size
          }
        }
        ).then(res => {
          console.log(res.data)
          that.tableData = res.data
        }
        )
      }
    },
    next() {
      const that = this
      this.cur++
      axios.get('https://localhost:7162/api/bookers/getallbookers', {
        params: {
          PageNumber: this.cur,
          PageSize: this.size
        }
      }
      ).then(res => {
        const array = res.data
        if (array === undefined || array === null || array.length <= 0) {
          this.cur--
          this.$message('已经是最后一页了！')
        } else {
          that.tableData = array
        }
      }
      )
    },

    show() {
      const that = this
      axios.get('https://localhost:7162/api/bookers/getallbookers', {
        params: {
          PageNumber: that.cur,
          PageSize: that.size
        }
      }, {}).then(res => {
        console.log(res.data)
        that.tableData = res.data
      })
    },

    cancelAddUser() {
      this.bookerForm = {
        bookerWx: '0',
        userName: '',
        bookerPwd: '',
        firstName: '',
        lastName: '',
        gender: '',
        phoneNum: '',
        dateOfBirth: ''
      }
    },

    modifyUser(val) {
      this.bookerForm = {
        bookerId: val.bookerId,
        userName: val.userName,
        bookerPwd: val.bookerPwd,
        firstName: val.firstName,
        lastName: val.lastName,
        gender: val.gender,
        phoneNum: val.phoneNum,
        dateOfBirth: val.dateOfBirth
      }
      this.dialogFormVisible = true
    },

    deleteUser(val) {
      var ID = val
      const that = this
      axios.delete('https://localhost:7162/api/bookers/deleteBooker', {
        params: {
          bookerId: ID
        }
      }, {}).then(res => {
        that.$message(res.status + '删除成功')
        that.tableData = []
        that.Data = []
        this.show()
      }
      )
    },

    addUser() {
      const that = this
      axios.post('https://localhost:7162/api/bookers', {
        userName: that.bookerForm.userName,
        bookerWx: '0',
        bookerPwd: that.bookerForm.bookerPwd,
        firstName: that.bookerForm.firstName,
        lastName: that.bookerForm.lastName,
        gender: that.bookerForm.gender,
        phoneNum: that.bookerForm.phoneNum,
        dateOfBirth: that.bookerForm.dateOfBirth
      }, {
      }).then(res => {
        if (res.status === 201) {
          that.$message('已创建成功')
          that.bookerForm = []
          that.dialogAddVisible = false
          this.show()
        } else {
          that.$message('创建失败')
        }
      }
      )
    },

    updateData() {
      const that = this
      axios.put('https://localhost:7162/api/bookers/updateBooker', {
        userName: that.bookerForm.userName,
        bookerWx: '0',
        bookerPwd: that.bookerForm.bookerPwd,
        firstName: that.bookerForm.firstName,
        lastName: that.bookerForm.lastName,
        gender: that.bookerForm.gender,
        phoneNum: that.bookerForm.phoneNum,
        dateOfBirth: that.bookerForm.dateOfBirth
      }, {
        params: {
          bookerId: that.bookerForm.bookerId
        }
      }).then(res => {
        if (res.status === 204) {
          that.$message('已提交成功')
          that.dialogFormVisible = false
          that.tableData = []
          this.show()
        } else {
          that.$message('提交失败')
        }
      }
      )
    },
    /* 布尔值格式化：cellValue为后台返回的值
*/
    formatGender: function(row, column, cellValue) {
      var ret = '' // 你想在页面展示的值
      if (cellValue==1) {
        ret = '男' // 根据自己的需求设定
      } else {
        ret = '女'
      }
      return ret
    }
  }
}
</script>
