__author__ = 'coco'

# 200行Python代码实现2048

import curses
from random import randrange, choice
from collections import defaultdict

# 所有的用户行为
actions = ['Up','Left','Down','Right','Restart','Exit']

# W(上) ，A（左），S（下），D（右），R（重置），Q（退出）
letter_codes = [ord(ch) for ch in 'WASDRQwasdrq']

# 输入与行为进行关联
actions_dict = dict(zip(letter_codes,actions*2))





def main(stdscr):
	def init():
		# 重置游戏棋盘
		game_field.reset()

		return 'Game'

	def not_game(state):
		# GameOver or Win
		game_field.draw(stdscr)
		# 读取用户输入 得到action，判断重启还是结束
		action = get_user_action(stdscr)

		responses = defaultdict(lambda:state) # 默认是当前状态，没有行为就会一直在当前界面循环
		responses['Restart'],responses['Exit'] = 'Init','Exit' #对应不同的行为转换到不同的状态
		return responses[action]

	def game():
		# 绘制当前棋盘状态
		game_field.draw(stdscr)

		# 读取用户输入得到action
		action = get_user_action(stdscr)

		if action == 'Restart':
			return 'Init'
		if action == 'Exit':
			return 'Exit'
		if game_field.move(action): #move successful
			if game_field.is_win():
				return 'Win'
			if game_field.is_gameover():
				return 'Gameover'
		return 'Game'

	state_actions = {
		'Init' : init,
		'Win' : lambda :not_game('Win'),
		'Gameover' : lambda:not_game('Gameover'),
		'Game' : game
	}

	curses.use_default_colors()
	game_field = GameField(win=32)

	state = 'Init'

	# 状态机开始循环
	while state != 'Exit':
		state = state_actions[state]()

# 阻塞+循环，直到输入有效
def get_user_action(keyboard):
	char = 'N'
	while char not in actions_dict:
		char = keyboard.getch()
	return actions_dict[char]

# 矩阵转置
def transpose(field):
	return [list(now) for row in zip(*field)]

# 矩阵逆转
def invert(field):
	return [row[::-1] for row in field]

# 创建棋盘
class GameField(object):
	def __init__(self,height=4,width=4,win=2048):
		self.height = height
		self.width = width
		self.win_value = 2048
		self.score = 0
		self.highscore = 0
		self.reset() # 棋盘重置
	
	# 随机生成一个2 or 4
	def spawn(self):
		new_element = 4 if randrange(100) > 89 else 2
		(i,j) = choice([(i,j) for i in range(self.width) for j in range(self.height) if self.field[i][j]==0])
		self.field[i][j] = new_element

	# 重置棋盘
	def reset(self):
		if self.score > self.highscore:
			highscore = self.score
		self.score=0
		self.field = [[0 for i in range(self.width)] for j in range(self.height)]
		self.spawn()
		self.spawn()

	# 棋盘走一步
	# 通过对矩阵进行转置与逆转，可以直接从左移得到其余三个方向的移动操作
	def move(self,direction): 
		# 一行向左合并
		def move_row_left(row):
			def tighten(row): # 把零散的非零单元挤到一块
				new_row = [i for i in row if i != 0]
				new_row += [0 for i in range(len(row) - len(new_row))]
				return new_row

			def merge(row):
				pair = False
				new_row = []
				for i in range(len(row)):
					if pair:
						new_row.append(2 * row[i])
						self.score += 2 * row[i]
						pair = False
					else:
						if i+1 < len(row) and row[i] == row[i+1]:
							pair = Ture
							new_row.append(0)
						else:
							new_row.append(row[i])
				assert len(new_row) == len(row)
				return new_row

				# 先挤到一块再合并再挤到一块
			return tighten(marge(tighten(row)))

		moves = {}
		moves['Left'] = lambda field: [move_row_left(row) for row in field]
		moves['Right'] = lambda field: invert(moves['Left'](invert(field)))
		moves['Up'] = lambda field: transpose(moves['Left'](transpose(field)))
		moves['Down'] = lambda field: transpose(moves['Right'](transpose(field)))

		if direction in moves:
			if self.move_is_possible(direction):
				self.field = moves[direction](self.field)
				self.spawn()
				return Ture
			else:
				return False

	def is_win(self):
		return any(any(i>= self.win_value for i in row) for row in self.field)

	def is_gameover(self):
		return not any(self.move_is_possible(move) for move in action)

	def move_is_possible(self,direction):
		def row_is_left_movable(row):
			def change(i):
				if row[i] == 0 and row[i+1] != 0: # 可以移动
					return True
				if row[i] != 0 and row[i+1] == row[i]: # 可以合并
					return True
				return False
			return any(change(i) for i in range(len(row)-1))

		check = {}
		check['Left'] = lambda field: any(row_is_left_movable(row) for row in field)
		check['Right'] = lambda field: check['Left'](invert(field))
		check['Up']    = lambda field: check['Left'](transpose(field))
		check['Down']  = lambda field: check['Right'](transpose(field))

		if direction in check:
			return check[direction](self.field)
		else:
			return False

    # 绘制游戏界面
	def draw(self,screen):
		help_str1 = '(W)Up (S)Down (A)Left (D)Right'
		help_str2 = '(R)Restart (Q)Exit'
		gameover_str = 'GAME OVER'
		win_str = 'YOU WIN!'

	def cast(string):
		screen.addstr(string + '\n')

    #绘制水平分割线
	def draw_hor_separator():
		line = '+' + ('+-----------' * self.width + '+')[1:]
		separator = defaultdict(lambda:line)
		if not hasattr(draw_hor_separator,"counter"):
			draw_hor_separator.counter = 0
		cast(separator[draw_hor_separator.counter])
		draw_hor_separator.counter += 1

	def draw_row(row):
		cast(''.join('|{: ^5} '.format(num) if num > 0 else '|     ' for num in row)+ '|')

	screen.clear()
	cast('SCORE: ' + str(self.score))

	if 0 != self.highscore:
		cast('HGHSCORE: '+ str(self.highscore))

	for row in self.field:
		draw_hor_separator()
		draw_row()

	draw_hor_separator()

	if self.is_win():
		cast(win_str)
	else:
		if self.is_gameover():
			cast(gameover_str)
		else:
			cast(help_str1)
	cast(help_str2)


# 运行 ###################################################
cursee.wrapper(main)


