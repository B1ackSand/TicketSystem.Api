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
        <el-form-item prop="bookerId" label="订票人ID">
          <el-input v-model="bookerForm.bookerId" />
        </el-form-item>

        <el-form-item prop="trainId" label="列车ID">
          <el-input v-model="bookerForm.trainId" />
        </el-form-item>

        <el-form-item prop="startTerminalId" label="起始站ID">
          <el-input v-model="bookerForm.startTerminalId" />
        </el-form-item>

        <el-form-item prop="endTerminalId" label="终点站ID">
          <el-input v-model="bookerForm.endTerminalId" />
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
        <el-form-item prop="bookerId" label="订票人ID">
          <el-input v-model="bookerForm.bookerId" />
        </el-form-item>

        <el-form-item prop="trainId" label="列车ID">
          <el-input v-model="bookerForm.trainId" />
        </el-form-item>

        <el-form-item prop="startTerminalId" label="起始站ID">
          <el-input v-model="bookerForm.startTerminalId" />
        </el-form-item>

        <el-form-item prop="endTerminalId" label="终点站ID">
          <el-input v-model="bookerForm.endTerminalId" />
        </el-form-item>

      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="cancelAddUser;dialogAddVisible = false;">返回</el-button>
        <el-button type="primary" @click="addUser()">添加</el-button>
      </div>
    </el-dialog>

    <el-table
      :data="tableData"
      stripe
      style="width: 100%"
    >
      <el-table-column
        prop="orderId"
        label="订单ID"
        width="300px"
      />

      <el-table-column
        prop="bookerId"
        label="订票人ID"
        width="300px"
      />

      <el-table-column
        prop="trainId"
        label="列车ID"
        width="300px"
      />

      <el-table-column
        prop="startTerminalId"
        label="起始站ID"
        width="300px"
      />

      <el-table-column
        prop="endTerminalId"
        label="终点站ID"
        width="300px"
      />

      <el-table-column
        prop="createdDate"
        label="订票时间"
        width="180px"
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
        orderId: '',
        bookerId: '',
        trainId: '',
        startTerminalId: '',
        endTerminalId: '',
        createdDate: ''
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
        axios.get('https://ticket.blacksand.top/api/getAllOrders', {
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
      axios.get('https://ticket.blacksand.top/api/getAllOrders', {
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
      axios.get('https://ticket.blacksand.top/api/getAllOrders', {
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
        orderId: '',
        bookerId: '',
        trainId: '',
        startTerminalId: '',
        endTerminalId: '',
        createdDate: ''
      }
    },

    modifyUser(val) {
      this.bookerForm = {
        orderId: val.orderId,
        bookerId: val.bookerId,
        trainId: val.trainId,
        startTerminalId: val.startTerminalId,
        endTerminalId: val.endTerminalId,
        createdDate: val.createdDate
      }
      this.dialogFormVisible = true
    },

    deleteUser(val) {
      var ID = val
      const that = this
      axios.delete('https://ticket.blacksand.top/api/orders/deleteOrder', {
        params: {
          bookerId: ID.bookerId,
          orderId: ID.orderId
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
      axios.post('https://ticket.blacksand.top/api/bookers/' + that.bookerForm.bookerId + '/orders', {
        trainId: that.bookerForm.trainId,
        startTerminalId: that.bookerForm.startTerminalId,
        endTerminalId: that.bookerForm.endTerminalId
      }, {
        params: {
          bookerId: that.bookerForm.bookerId
        }
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
      axios.put('https://ticket.blacksand.top/api/orders/updateOrder', {
        trainId: that.bookerForm.trainId,
        startTerminalId: that.bookerForm.startTerminalId,
        endTerminalId: that.bookerForm.endTerminalId
      }, {
        params: {
          orderId: that.bookerForm.orderId,
          bookerId: that.bookerForm.bookerId
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
