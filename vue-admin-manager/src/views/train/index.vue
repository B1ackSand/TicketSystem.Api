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
        <el-form-item prop="lineId" label="路线ID">
          <el-input v-model="bookerForm.lineId" />
        </el-form-item>

        <el-form-item prop="trainName" label="列车编号">
          <el-input v-model="bookerForm.trainName" />
        </el-form-item>

        <el-form-item prop="typeOfTrain" label="列车类型">
          <el-input v-model="bookerForm.typeOfTrain" />
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
        <el-form-item prop="lineId" label="路线ID">
          <el-input v-model="bookerForm.lineId" />
        </el-form-item>

        <el-form-item prop="trainName" label="列车编号">
          <el-input v-model="bookerForm.trainName" />
        </el-form-item>

        <el-form-item prop="typeOfTrain" label="列车类型">
          <el-input v-model="bookerForm.typeOfTrain" />
        </el-form-item>

      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="cancelAddUser;dialogAddVisible = false;">返回</el-button>
        <el-button type="primary" @click="addUser">添加</el-button>
      </div>
    </el-dialog>

    <el-table
      :data="tableData"
      stripe
      style="width: 100%"
    >
      <el-table-column
        prop="trainId"
        label="列车ID"
        width="300px"
      />

      <el-table-column
        prop="lineId"
        label="路线ID"
        width="300px"
      />

      <el-table-column
        prop="trainName"
        label="列车编号"
        width="180px"
      />

      <el-table-column
        prop="typeOfTrain"
        label="列车类型"
        width="680px"
      />

      <el-table-column label="修改操作" align="center" min-width="150" width="200">
        <template slot-scope="scope">
          <el-button type="info" @click="modifyUser(scope.row);dialogFormVisible = true;">修改</el-button>
          <el-button type="info" @click="deleteUser(scope.row)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>

    <el-table-column label="操作" align="center" min-width="150">
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
        trainId: '',
        lineId: '',
        trainName: '',
        typeOfTrain: ''
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
        this.$message('已经是第一页了')
      } else {
        this.cur--
        axios.get('https://ticket.blacksand.top/api/getAllTrains', {
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
      axios.get('https://ticket.blacksand.top/api/getAllTrains', {
        params: {
          PageNumber: this.cur,
          PageSize: this.size
        }
      }
      ).then(res => {
        const array = res.data
        if (array === undefined || array === null || array.length <= 0) {
          this.cur--
          this.$message('已经是最后一页了')
        } else {
          that.tableData = array
        }
      }
      )
    },

    show() {
      const that = this
      axios.get('https://ticket.blacksand.top/api/getAllTrains', {
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
        trainId: '',
        lineId: '',
        trainName: '',
        typeOfTrain: ''
      }
    },

    modifyUser(val) {
      this.bookerForm = {
        trainId: val.trainId,
        lineId: val.lineId,
        trainName: val.trainName,
        typeOfTrain: val.typeOfTrain
      }
    },

    deleteUser(val) {
      var ID = val
      const that = this
      axios.delete('https://ticket.blacksand.top/api/trains/deleteTrain', {
        params: {
          trainId: ID.trainId
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
      axios.post('https://ticket.blacksand.top/api/lines/' + that.bookerForm.lineId + '/trains', {
        trainName: that.bookerForm.trainName,
        typeOfTrain: that.bookerForm.typeOfTrain
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
      axios.put('https://ticket.blacksand.top/api/trains/updateTrain', {
        trainName: that.bookerForm.trainName,
        typeOfTrain: that.bookerForm.typeOfTrain,
        lineId: that.bookerForm.lineId
      }, {
        params: {
          trainId: that.bookerForm.trainId
        }
      }).then(res => {
        if (res.status === 204 || res.status === 201) {
          that.$message('已提交成功')
          that.dialogFormVisible = false
          that.tableData = []
          this.show()
        } else {
          that.$message('提交失败')
        }
      }
      )
    }
  }
}
</script>
